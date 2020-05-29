using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaFotografa.DomainModel.Entities;
using SistemaFotografa.DomainModel.Interfaces.Services;
using SistemaFotografa.Presentation.Controllers;
using static System.Collections.Specialized.BitVector32;

namespace SistemaFotografa.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly IFotoService _fotoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session=> _httpContextAccessor.HttpContext.Session;

        public DefaultController(IHttpContextAccessor httpContextAccessor, IFotoService fotoService,IClienteService clienteService)
        {
            _clienteService = clienteService;
            _httpContextAccessor = httpContextAccessor;
            _fotoService = fotoService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Responsivo()
        {
            ViewBag.Fotos = _fotoService.BuscarTodosDoAlbumPeloNome("Trabalhos").ToList();
            return View();
        }
        
            
            
      
        public IActionResult LoginSistema(String UserName,String Password)
        {
            var cliente = _clienteService.BuscarPeloUsuario(UserName);
            _session.Set<Cliente>("cliente",cliente);
            
            if (Password == cliente.Login.Password)
            {
                if (cliente.Login.Administrador == true)
                {
                    return RedirectToAction("SistemaAdministrador");
                }
                else
                {
                    return RedirectToAction("SistemaCliente");
                }
            }
            return RedirectToAction("Responsivo");
        }
        public IActionResult SistemaCliente()
        {
            
            return View();
            
            
            
        }
        public IActionResult VoltarAoSite()
        {
            _session.Set<Cliente>("cliente", null);
            return RedirectToAction("Responsivo", "Default");
        }
        public IActionResult SistemaAdministrador(String UserName,String Password)
        {
            try
            {
                if (!_session.Get<Cliente>("cliente").Login.Administrador)
                {
                    return RedirectToAction("VoltarAoSite");
                }
            }
            catch 
            {
                return RedirectToAction("VoltarAoSite");
            }
            return View();
        }
        public IActionResult Redirecionar(string controllerAction)
        {
            
            
            return RedirectToAction(controllerAction.Split("/")[1].ToString(), controllerAction.Split("/")[0].ToString());
        }
    }
}
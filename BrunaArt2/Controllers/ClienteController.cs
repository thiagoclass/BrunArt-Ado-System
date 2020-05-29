using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaFotografa.DomainModel.Entities;

using SistemaFotografa.DomainModel.Interfaces.Services;


namespace SistemaFotografa.Presentation.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public ClienteController(IClienteService clienteService, IHttpContextAccessor httpContextAccessor)
        {
            _clienteService = clienteService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult VoltarAoSite()
        {
            _session.Set<Cliente>("cliente", null);
            return RedirectToAction("Responsivo", "Default");
        }

        public IActionResult Index()
        {
            if (_session.GetString("Alertas") != null)
            {
                ViewBag.Alerta = _session.GetString("Alertas");
                _session.Remove("Alertas");
            }
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
            return View(_clienteService.BuscarTodos());
        }
        public IActionResult Cadastro()
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
        public IActionResult Editar(Guid id)
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
            return View(_clienteService.Buscar(id));
        }
        public IActionResult AtualizarCliente(Cliente cliente)
        {

            _clienteService.Atualizar(cliente);
            _session.SetString("Alertas", "Parabéns!!!| Você acabou de atualizar um cliente.");
            return RedirectToAction("Index");
        }
        public IActionResult RemoverCliente(Cliente cliente)
        {

            _clienteService.Deletar(cliente.Id);
            _session.SetString("Alertas", "Parabéns!!!| Você acabou de excluir um cliente.");
            return RedirectToAction("Index");

        }
        public IActionResult Remover(Guid id)
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

            return View(_clienteService.Buscar(id));
        }
        public IActionResult CadastrarNovo(Cliente cliente)
        {

            _clienteService.Criar(cliente);
            _session.SetString("Alertas", "Parabéns!!!| Você acabou de cadastrar um cliente.");
            return RedirectToAction("Index");
        }
    }
}
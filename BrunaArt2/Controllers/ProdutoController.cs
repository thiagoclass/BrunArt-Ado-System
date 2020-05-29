using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaFotografa.DomainModel.Entities;
using SistemaFotografa.DomainModel.Interfaces.Repositories;
using SistemaFotografa.DomainModel.Interfaces.Services;

namespace SistemaFotografa.Presentation.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public ProdutoController(IHttpContextAccessor httpContextAccessor, IProdutoService produtoService)
        {
            _produtoService = produtoService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult VoltarAoSite()
        {
            _session.Set<Cliente>("cliente", null);
            return RedirectToAction("Responsivo", "Default");
        }

        public IActionResult CadastrarNovo(Produto produto)
        {

            _produtoService.Criar(produto);
            _session.SetString("Alertas", "Muito bem!!!|Você acabou de cadastrar um produto!");
            return RedirectToAction("Index");
        }

        public IActionResult AtualizarProduto(Produto produto)
        {

            _produtoService.Atualizar(produto);
            _session.SetString("Alertas", "Muito bem!!!|Você acabou de atualizar informações de um Produto!");
            return RedirectToAction("Index");
        }

        public IActionResult RemoverProduto(Produto produto)
        {
            produto = _produtoService.Buscar(produto.Id);
            _produtoService.Deletar(produto.Id);
            _session.SetString("Alertas", "Muito bem!!!|Você acabou de excluir um Produto!");
            return RedirectToAction("Index");

        }

        // GET: Produto
        public ActionResult Index()
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
            return View(_produtoService.BuscarTodos());
        }

        
        public ActionResult Create()
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


        
        public ActionResult Edit(Guid id)
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
            return View(_produtoService.Buscar(id));
        }


        
        public ActionResult Delete(Guid id)
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
            return View(_produtoService.Buscar(id));
        }

        
    }
}
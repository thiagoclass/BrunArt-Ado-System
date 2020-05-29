using System;
using System.Collections.Generic;
using System.IO;
using System.Web; 
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaFotografa.DomainModel.Entities;
using SistemaFotografa.DomainModel.Interfaces.Repositories;
using AmazingBank.Infrastructure.AzureStorage;
using SistemaFotografa.DomainService;
using SistemaFotografa.DomainModel.Interfaces.Services;

namespace SistemaFotografa.Presentation.Controllers
{
    public class FotoController : Controller
    {
        private readonly IFotoService _fotoService;
        private readonly IAlbumService _albumService;
        private readonly IFileServerService _fileServerService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        

        // GET: Foto
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
            return View(_fotoService.BuscarTodos());
        }

        public IActionResult VoltarAoSite()
        {
            _session.Set<Cliente>("cliente", null);
            return RedirectToAction("Responsivo", "Default");
        }

        public IActionResult Create()
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
            var albuns = _albumService.BuscarTodos();
            ViewBag.Albuns = albuns;

            return View();
        }
        //public IActionResult IndexSelect()
        //{
            
        //    var albuns = _albumService.BuscarTodosDoCliente(_session.Get<Cliente>("cliente"));
        //    ViewBag.FotosPreSelecionadas = _session.Get<List<Foto>>("FotosPreSelecionadas");
        //    ViewBag.FotosSelecionadas = _session.Get<List<Foto>>("FotosSelecionadas");
        //    if (ViewBag.FotosPreSelecionadas == null)
        //    {
        //        List<Foto> fotos = new List<Foto>();
        //        foreach (var album in albuns)
        //        {
        //            fotos.AddRange(_fotoService.ReadAllPreselect(album));
        //        }
        //        ViewBag.FotosPreSelecionadas = fotos;
        //    }
        //    if (ViewBag.FotosSelecionadas == null)
        //    {
        //        List<Foto> fotos = new List<Foto>();
        //        foreach (var album in albuns)
        //        {
        //            fotos.AddRange(_fotoService.ReadAllSelect(album));
        //        }
        //        ViewBag.FotosSelecionadas = fotos;
        //    }
        //    _session.Set<List<Foto>>("FotosPreSelecionadas", (List<Foto>)ViewBag.FotosPreSelecionadas);
        //    _session.Set<List<Foto>>("FotosSelecionadas", (List<Foto>)ViewBag.FotosSelecionadas); 
        //    return View();
        //}
        //public IActionResult SelectFoto(Guid select)
        //{
        //    ViewBag.FotosPreSelecionadas = _session.Get<List<Foto>>("FotosPreSelecionadas");
        //    ViewBag.FotosSelecionadas = _session.Get<List<Foto>>("FotosSelecionadas");
        //    List<Foto> fotosPreSelecionadas = ViewBag.FotosPreSelecionadas;
        //    List<Foto> fotosSelecionadas = ViewBag.FotosSelecionadas;
        //    foreach (var foto in fotosPreSelecionadas)
        //    {
        //        if(foto.Id == select)
        //        {
        //            foto.Situacao = 1;
        //            fotosSelecionadas.Add(foto);
        //        }
        //    }
        //    foreach (var foto in fotosSelecionadas)
        //    {
        //        fotosPreSelecionadas.Remove(foto);
        //    }
        //    ViewBag.FotosPreSelecionadas= fotosPreSelecionadas;
        //    ViewBag.FotosSelecionadas= fotosSelecionadas;
        //    _session.Set<List<Foto>>("FotosPreSelecionadas", (List<Foto>)ViewBag.FotosPreSelecionadas);
        //    _session.Set<List<Foto>>("FotosSelecionadas", (List<Foto>)ViewBag.FotosSelecionadas);
            
        //    return View("IndexSelect");
        //}
        //public IActionResult UnSelectFoto(Guid select)
        //{
        //    ViewBag.FotosPreSelecionadas = _session.Get<List<Foto>>("FotosPreSelecionadas");
        //    ViewBag.FotosSelecionadas = _session.Get<List<Foto>>("FotosSelecionadas");
        //    List<Foto> fotosPreSelecionadas = ViewBag.FotosPreSelecionadas;
        //    List<Foto> fotosSelecionadas = ViewBag.FotosSelecionadas;
        //    foreach (var foto in fotosSelecionadas)
        //    {
        //        if (foto.Id == select)
        //        {
        //            foto.Situacao = 0;
        //            fotosPreSelecionadas.Add(foto);
        //        }
        //    }
        //    foreach (var foto in fotosPreSelecionadas)
        //    {
        //        fotosSelecionadas.Remove(foto);
        //    }
        //    ViewBag.FotosPreSelecionadas = fotosPreSelecionadas;
        //    ViewBag.FotosSelecionadas = fotosSelecionadas;
        //    _session.Set<List<Foto>>("FotosPreSelecionadas", (List<Foto>)ViewBag.FotosPreSelecionadas);
        //    _session.Set<List<Foto>>("FotosSelecionadas", (List<Foto>)ViewBag.FotosSelecionadas);
        //    return View("IndexSelect");
        //}
        
        //public IActionResult CadastrarSelecaoFotos()
        //{
        //    ViewBag.FotosPreSelecionadas = _session.Get<List<Foto>>("FotosPreSelecionadas");
        //    ViewBag.FotosSelecionadas = _session.Get<List<Foto>>("FotosSelecionadas");
        //    List<Foto> fotosPreSelecionadas = ViewBag.FotosPreSelecionadas;
        //    List<Foto> fotosSelecionadas = ViewBag.FotosSelecionadas;
            
        //    foreach (var foto in fotosSelecionadas)
        //    {
        //        _fotoService.AtualizarSituacao(foto);
        //    }
        //    foreach (var foto in fotosPreSelecionadas)
        //    {
        //        _fotoService.AtualizarSituacao(foto);
        //    }
        //    _session.Set<List<Foto>>("FotosPreSelecionadas", null);
        //    _session.Set<List<Foto>>("FotosSelecionadas", null);
        //    _session.SetString("Alertas", "Parabéns!!!| Você acabou de cadastrar a seleção de fotos do seu album.");

        //    return RedirectToAction("SistemaCliente","Default");
        //}

        public FotoController(IFotoService fotoService, IAlbumService albumService, IHttpContextAccessor httpContextAccessor,IFileServerService fileServerService)
        {
            _fotoService = fotoService;
            _albumService = albumService;
            _fileServerService = fileServerService;
            _httpContextAccessor = httpContextAccessor;
        }
        

        public async Task<IActionResult> CadastrarNovo(Foto foto, IFormFile imagem)
        {
            byte[] miniImagemByte;
            
            using (var memoryStream = new MemoryStream())
            {
                await imagem.CopyToAsync(memoryStream);

                miniImagemByte = EditorDeImagem.DrawSize(memoryStream.ToArray(), 256);

            }
            foto.FotoUrl = _fileServerService.UploadFile("_Foto", imagem.OpenReadStream(), SistemaFotografa.InfraStructure.AzureStorage.Properties.Resources.AzureBlobContainer, imagem.ContentType, foto.Album.Id.ToString());
            foto.MiniFotoUrl = _fileServerService.UploadFile("_MiniFoto", new MemoryStream(miniImagemByte), SistemaFotografa.InfraStructure.AzureStorage.Properties.Resources.AzureBlobContainer, imagem.ContentType, foto.Album.Id.ToString());
            _fotoService.Criar(foto);
            _session.SetString("Alertas", "Muito bem!!!|Você acabou de cadastrar uma foto!");
            return RedirectToAction("Index");
        }
        
        

        public IActionResult Edit(Guid id)
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
            return View(_fotoService.Buscar(id));
        }

        


        public IActionResult Delete(Guid id)
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
            return View(_fotoService.Buscar(id));
            
        }
        public IActionResult AtualizarFoto(Foto foto)
        {

            _fotoService.Atualizar(foto);
            _session.SetString("Alertas", "Muito bem!!!|Você acabou de atualizar informações de uma foto!");
            return RedirectToAction("Index");
        }
        public IActionResult RemoverFoto(Foto foto)
        {
            foto = _fotoService.Buscar(foto.Id);
            _fotoService.Deletar(foto.Id);
            _fileServerService.DeleteFile(SistemaFotografa.InfraStructure.AzureStorage.Properties.Resources.AzureBlobContainer, foto.FotoUrl.Substring(50).Split("_")[0].ToString());
            _session.SetString("Alertas", "Muito bem!!!|Você acabou de excluir uma foto!");
            return RedirectToAction("Index");

        }
        
    }
}
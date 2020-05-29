using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaFotografa.DomainModel.Entities;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using SistemaFotografa.DomainService;
using SistemaFotografa.DomainModel.Interfaces.Services;

namespace SistemaFotografa.Presentation.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IFotoService _fotoService;
        private readonly IFileServerService _fileServerService;
        private readonly IClienteService _clienteService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        

        public AlbumController(IAlbumService albumService, IClienteService clienteService, IFotoService fotoService, IHttpContextAccessor httpContextAccessor, IFileServerService fileServerService)
        {
            _albumService = albumService;
            _clienteService = clienteService;
            _fotoService = fotoService;
            _httpContextAccessor = httpContextAccessor;
            _fileServerService = fileServerService;
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
            return View(_albumService.BuscarTodos());
        }

        public IActionResult VoltarAoSite()
        {
            _session.Set<Cliente>("cliente", null);
            return RedirectToAction("Responsivo", "Default");
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
            var clientes = _clienteService.BuscarTodos();
            ViewBag.Clientes = clientes;
            Album album = new Album();
            return View(album);
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
            return View(_albumService.Buscar(id));
        }
        public async Task<IActionResult> AdicionarFotoDropZone(Guid id)
        {
            byte[] miniImagemByte;
            foreach (var imagem in Request.Form.Files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagem.CopyToAsync(memoryStream);

                    miniImagemByte = EditorDeImagem.DrawSize(memoryStream.ToArray(),256);

                }
                
                Foto foto = new Foto();
                foto.Id = Guid.NewGuid();
                foto.Album.Id = id;
                foto.Descricao = String.Empty;
                foto.Nome = String.Empty;
                foto.FotoUrl = _fileServerService.UploadFile("_Foto", imagem.OpenReadStream(), SistemaFotografa.InfraStructure.AzureStorage.Properties.Resources.AzureBlobContainer, imagem.ContentType, foto.Album.Id.ToString());
                foto.MiniFotoUrl = _fileServerService.UploadFile("_MiniFoto", new MemoryStream(miniImagemByte), SistemaFotografa.InfraStructure.AzureStorage.Properties.Resources.AzureBlobContainer, imagem.ContentType, foto.Album.Id.ToString());
                _fotoService.CriarComId(foto);

            }
            return Ok();
        }
        
        public IActionResult AtualizarAlbum(Album album)
        {

            _albumService.Atualizar(album);
            _session.SetString("Alertas", "Parabéns!!!| Você acabou de atualizar um album.");
            return RedirectToAction("Index");
        }
        public IActionResult RemoverAlbum(Album album)
        {

            _albumService.Deletar(album.Id);
            _session.SetString("Alertas", "Parabéns!!!| Você acabou de excluir um album.");
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
            return View(_albumService.Buscar(id));
        }
        public IActionResult CadastrarNovo(Album album)
        {

            _albumService.Criar(album);
            _session.SetString("Alertas", "Parabéns!!!| Você acabou de cadastrar um album.");
            return RedirectToAction("Index");
        }
    }
}
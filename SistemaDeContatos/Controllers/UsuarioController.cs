using Microsoft.AspNetCore.Mvc;
using SistemaDeContatos.Filters;
using SistemaDeContatos.Models.Entities;
using SistemaDeContatos.Services;

namespace SistemaDeContatos.Controllers
{
    [PaginaParaUsuarioLogadoAdmin]
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public IActionResult Index()
        {
            
           var usuarios = _usuarioService.FindAll();
            return View(usuarios);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioService.AddUsuario(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);


        }

        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var usuario = _usuarioService.FindById(id.Value);
            if(usuario == null)
            {
                return NotFound();
            }
          
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Edit(UsuarioSemSenha usuarioSemSenha)
        {
            try
            {
                Usuario usuario = null;
                if (ModelState.IsValid)
                {
                    usuario = new Usuario()
                    {
                        Id = usuarioSemSenha.Id,
                        Login = usuarioSemSenha.Login,
                        Email = usuarioSemSenha.Email,
                        Nome = usuarioSemSenha.Nome,
                        Perfil = usuarioSemSenha.Perfil

                    };
                    _usuarioService.Update(usuario);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return View();




        }

        public IActionResult Delet(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
           var usuario =  _usuarioService.FindById(id.Value);
            if(usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Delet(Usuario usuario)
        {
            if(usuario == null)
            {
                return NotFound();
            }
            _usuarioService.DeletUsuario(usuario);
            return RedirectToAction(nameof(Index));
        }
    }
}

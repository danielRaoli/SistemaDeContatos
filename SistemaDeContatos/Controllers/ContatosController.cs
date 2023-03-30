using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using SistemaDeContatos.Filters;
using SistemaDeContatos.Helper;
using SistemaDeContatos.Models.Entities;
using SistemaDeContatos.Services;

namespace SistemaDeContatos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ContatosController : Controller
    {
        private readonly ContatoService _contatosService;
        private readonly ISessao _sessao;
        public ContatosController(ContatoService contatoService, ISessao sessao)
        {
            _contatosService = contatoService;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            var usuarioSessao = _sessao.BuscarSessao();
            var contatos = _contatosService.FindAll(usuarioSessao.Id);
            return View(contatos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuarioSessao = _sessao.BuscarSessao();
                    contato.UsuarioId = usuarioSessao.Id;

                    _contatosService.AddContato(contato);
                    TempData["alertSucess"] = "Contato Cadastrado com sucesso";
                    return RedirectToAction(nameof(Index));

                }
                TempData["alertError"] = "Erro na model, tente novamente";
                return View();
            }
            catch(Exception ex)
            {
                TempData["alertError"] = ex.Message;
                return View();
            }

        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contato = _contatosService.FindById(id.Value);
            if (contato == null)
            {
                return NotFound();
            }
            return View(contato);
        }

        [HttpPost]
        public IActionResult Edit(Contato contato)
        {
            if (ModelState.IsValid)
            {
                _contatosService.Update(contato);
                return RedirectToAction(nameof(Index));
            }
            return View(contato);

        }

        public IActionResult Delet(int? id)
        {
            if (id == null)
            {
                throw new Exception("Id not provided");
            }
            var contato = _contatosService.FindById(id.Value);
            if (contato == null)
            {
                return NotFound();
            }

            return View(contato);

        }

        [HttpPost]
        public IActionResult Delet(Contato contato)
        {
            _contatosService.DeletContato(contato);
            return RedirectToAction(nameof(Index));
        }

    }
}

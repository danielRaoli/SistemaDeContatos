using Microsoft.AspNetCore.Mvc;
using SistemaDeContatos.Filters;
using SistemaDeContatos.Helper;
using SistemaDeContatos.Models.Entities;
using SistemaDeContatos.Models.ViewModels;
using SistemaDeContatos.Services;

namespace SistemaDeContatos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class AlterarSenhaController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly ISessao _sessao;
        public AlterarSenhaController(UsuarioService usuarioService, ISessao sessao)
        {
            _usuarioService = usuarioService;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(AlterSenhaViewModel alterarSenhaModel)
        {
            try
            {
              Usuario usuarioSessao =  _sessao.BuscarSessao();
                alterarSenhaModel.Id = usuarioSessao.Id; 
                if (ModelState.IsValid)
                {
                    _usuarioService.AlterarSenha(alterarSenhaModel);
                    TempData["alertSucess"] = "Senha atualizada com sucesso";
                    return View("Index", alterarSenhaModel);
                }
                return View("Index", alterarSenhaModel);
            }
            catch(Exception e)
            {
                TempData["alertError"] = e.Message;
                return View("Index", alterarSenhaModel);
            }
           
        }
    }
}

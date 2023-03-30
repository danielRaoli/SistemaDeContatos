using Microsoft.AspNetCore.Mvc;
using SistemaDeContatos.Data;
using SistemaDeContatos.Helper;
using SistemaDeContatos.Models.ViewModels;
using SistemaDeContatos.Services;

namespace SistemaDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(UsuarioService usuarioService, ISessao sessao, IEmail email)
        {
            _usuarioService = usuarioService;
            _sessao = sessao;
            _email = email;
        }
        public IActionResult Index()
        {
            if(_sessao.BuscarSessao() != null) return RedirectToAction(nameof(Index), "Home");
            return View();
        }

        [HttpPost]
        public IActionResult Logar(LoginViewModel loginView)
        {
            if (ModelState.IsValid)
            {
                var obj = _usuarioService.FindByLogin(loginView.Login);
                if (obj != null)
                {
                    if (obj.ValidaSenha(loginView.Senha))
                    {
                        _sessao.CriarSessao(obj);   
                        return RedirectToAction(nameof(Index), "Home");
                    }
                    TempData["alertError"] = "senha do usuário está incorreto";
                    return View("Index");
                }
                TempData["alertError"] = "Login e/ou senha do usuário está incorreto";
                return View("Index");
            }
            TempData["alertError"] = "Login e/ou senha do usuário está incorreto";
            return View("Index");
        }

        public IActionResult Redefinir()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Redefinir(RedefinirSenhaViewModel redefinir)
        {
            if (ModelState.IsValid)
            {
              var usuario =  _usuarioService.FindByLoginEmail(redefinir.Login, redefinir.Email);
              if(usuario != null)
                {
                    string novaSenha = usuario.GerarNovaSenha();
                    string mensagem = $"Sua nova senha é {novaSenha}";
                    bool enviado = _email.Enviar(usuario.Email, "Sistema de contatos - Nova Senha",mensagem);
                    if (enviado)
                    {
                        _usuarioService.Update(usuario);
                        TempData["alertSucess"] = "Senha redefinida com sucesso!";
                    }
                    else
                    {
                        TempData["alertError"] = "Não foi possível enviar o email, tente novamente";
                    }
                    
                    return RedirectToAction(nameof(Index));
                }
                TempData["alertError"] = "Usuário ou email não encontrados!";
                return View();
            }
            TempData["alertError"] = "Usuário ou email não encontrados!";
            return View();
        }
       
        public IActionResult Logout()
        {
            _sessao.RemoverSessao();
            return RedirectToAction(nameof(Index));
        }
    }
}

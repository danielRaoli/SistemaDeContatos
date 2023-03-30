using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaDeContatos.Models.Entities;

namespace SistemaDeContatos.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string usuarioSessao = HttpContext.Session.GetString("sessaoUsuario");

            if (string.IsNullOrEmpty(usuarioSessao)) return null;

            var usuario = JsonConvert.DeserializeObject<Usuario>(usuarioSessao);
            return View(usuario);
        }
    }
} 

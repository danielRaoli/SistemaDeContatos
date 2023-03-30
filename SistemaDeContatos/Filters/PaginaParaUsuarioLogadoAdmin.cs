using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SistemaDeContatos.Models.Entities;

namespace SistemaDeContatos.Filters
{
    public class PaginaParaUsuarioLogadoAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string usuarioSessao = context.HttpContext.Session.GetString("sessaoUsuario");
            if (string.IsNullOrEmpty(usuarioSessao))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { {"controller","Login" },{"view","Index" } });
            }
            else
            {
                Usuario usuario = JsonConvert.DeserializeObject<Usuario>(usuarioSessao);
                if(usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "view", "Index" } });
                }
                if(usuario.Perfil != Models.Enums.PerfilEnum.Admin)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "view", "Index" } });
                }
            }
            base.OnActionExecuting(context);
        }
    }
}

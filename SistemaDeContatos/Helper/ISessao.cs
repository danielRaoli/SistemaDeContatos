using SistemaDeContatos.Models.Entities;

namespace SistemaDeContatos.Helper
{
    public interface ISessao
    {
        void CriarSessao(Usuario usuario);

        void RemoverSessao();

        Usuario BuscarSessao();
    }
}

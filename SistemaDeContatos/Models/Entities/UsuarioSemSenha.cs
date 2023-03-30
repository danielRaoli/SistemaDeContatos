using SistemaDeContatos.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeContatos.Models.Entities
{
    public class UsuarioSemSenha
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite um nome")]
        [StringLength(maximumLength: 50, MinimumLength = 4, ErrorMessage = "O nome tem que ter pelo menos 4 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite um login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite um email")]
        [EmailAddress(ErrorMessage = "Digite um email válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Escolha um perfil")]
        public PerfilEnum? Perfil { get; set; }


        public UsuarioSemSenha()
        {

        }
        public UsuarioSemSenha(string nome, string login, string email,PerfilEnum perfil)
        {
            Nome = nome;
            Login = login;
            Email = email;
            Perfil = perfil;

        }
    }
}

using SistemaDeContatos.Helper;
using SistemaDeContatos.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SistemaDeContatos.Models.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Digite um nome")]
        [StringLength(maximumLength: 50,MinimumLength =4, ErrorMessage ="O nome tem que ter pelo menos 4 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Digite um login")]
        public string Login { get; set; }

        [Required(ErrorMessage ="Digite uma senha")]
        public string Senha { get; set; }
        [Required(ErrorMessage ="Digite um email")]
        [EmailAddress(ErrorMessage ="Digite um email válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage ="Escolha um perfil")]
        public PerfilEnum? Perfil { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public List<Contato> contatos { get; set; } = new List<Contato>(); 


        public Usuario()
        {
                
        }
        public Usuario(string nome, string login, string email, string senha, PerfilEnum perfil, DateTime dataCriacao)
        {
            Nome = nome;
            Login = login;
            Email = email;
            Perfil = perfil;
            Senha = senha;
            DataCriacao = dataCriacao;

        }

        public bool ValidaSenha(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenha()
        {
            Senha = Senha.GerarHash();
        }

        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
        }

        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }

    }
}

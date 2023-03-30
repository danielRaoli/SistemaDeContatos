using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace SistemaDeContatos.Models.Entities
{
    public class Contato
    {
        public int Id { get; set; }

        [Display(Name ="Nome")]
        [Required(ErrorMessage ="Digite um nome")]
        [StringLength(maximumLength:100, MinimumLength =3,ErrorMessage ="digite um nome válido")]
        public string Nome { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Digite um Email")]
        [EmailAddress(ErrorMessage ="Digite um email válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Celular")]
        [Required(ErrorMessage = "Digite um Numero")]
        [StringLength(9, ErrorMessage = "digite um nome válido")]
        public string Celular { get; set; }
        
        public int? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

       

        public Contato()
        {

        }

        public Contato(string nome, string email, string celular)
        {
            Nome = nome;
            Email = email;
            Celular = celular;

        }
    }
}

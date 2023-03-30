using System.ComponentModel.DataAnnotations;

namespace SistemaDeContatos.Models.ViewModels
{
    public class RedefinirSenhaViewModel
    {
        [Required(ErrorMessage = "Digite o login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Digite um email válido")]
        public string Email { get; set; }
    }
}

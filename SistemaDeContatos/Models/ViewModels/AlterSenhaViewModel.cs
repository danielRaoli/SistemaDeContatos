using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace SistemaDeContatos.Models.ViewModels
{
    public class AlterSenhaViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Digite a senha atual do usuário")]
        public string SenhaAtual { get; set; }
        [Required(ErrorMessage ="Digitea nova senha do usuário ")]
        public string NovaSenha { get; set; }
        [Required(ErrorMessage ="Confirme a nova senha")]
        [Compare("NovaSenha")]
        public string ConfirmarNovaSenha { get; set; }
    }
}

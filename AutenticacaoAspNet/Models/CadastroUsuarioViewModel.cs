using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AutenticacaoAspNet.Models
{
    public class CadastroUsuarioViewModel
    {
        [Required(ErrorMessage = "Informe seu nome")]
        [MaxLength(100, ErrorMessage = "O nome deve possuir até 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe seu login")]
        [MaxLength(50, ErrorMessage = "O login deve possuir até 50 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve possuir no mínimo 6 caracteres")]
        public string Senha { get; set; }

        [Display(Name = "Confirmar Senha")]
        [Required(ErrorMessage = "Confirme sua senha")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve possuir no mínimo 6 caracteres")]
        [Compare(nameof(Senha), ErrorMessage = "As senhas não se coincidem")]
        public string ConfirmacaoSenha { get; set; }
    }
}
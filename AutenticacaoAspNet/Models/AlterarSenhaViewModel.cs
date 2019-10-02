using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutenticacaoAspNet.Models
{
    public class AlterarSenhaViewModel
    {
        [Required(ErrorMessage = "Informe sua senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        [MinLength(6, ErrorMessage = "A senha deve possuir no mínimo 6 caracteres")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        [MinLength(6, ErrorMessage = "A senha deve possuir no mínimo 6 caracteres")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [MinLength(6, ErrorMessage = "A senha deve possuir no mínimo 6 caracteres")]
        [Compare(nameof(NovaSenha), ErrorMessage = "As senhas não se coincidem")]
        public string ConfirmacaoSenha { get; set; }
    }
}
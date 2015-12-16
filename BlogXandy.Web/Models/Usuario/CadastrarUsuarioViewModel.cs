using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogXandy.Web.Models.Usuario
{
    public class CadastrarUsuarioViewModel
    {

        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatorio.")] //campo obrigatorio
        [StringLength(100, MinimumLength = 3, ErrorMessage = "A quantidade de caracteres no campo Nome deve ser entre {2} e {1}")]
        public string Nome { get; set; }

        [DisplayName("Login")]
        [Required(ErrorMessage = "O campo Login é obrigatorio.")] //campo obrigatorio
        [StringLength(30, MinimumLength = 6, ErrorMessage = "A quantidade de caracteres no campo Login deve ser entre {2} e {1}")]
        public string Login { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "O campo Senha é obrigatorio.")] //campo obrigatorio
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A quantidade de caracteres no campo Senha deve ser entre {2} e {1}")]
        public string Senha { get; set; }

        [DisplayName("Confirmar senha")]
        [Required(ErrorMessage = "O campo Senha é obrigatorio.")] //campo obrigatorio
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A quantidade de caracteres no campo confimar deve ser entre {2} e {1}")]
        [Compare("Senha", ErrorMessage = "As senhas digitadas não conferem.")]
        public string ConfirmarSenha { get; set; }
    }

  
}
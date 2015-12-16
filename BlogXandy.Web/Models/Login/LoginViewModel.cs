using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogXandy.Web.Models.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo login é obrigatorio")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatorio")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Display(Name = "Lembrar?")]
        public Boolean Lembrar { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogXandy.Web.Models.Administracao
{
    public class CadastrarPostViewModel
    {
        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage = "O campo Título é obrigatorio.")] //campo obrigatorio
        [StringLength(100,MinimumLength =2, ErrorMessage = "A quantidade de caracteres no campo título deve ser entre {2} e {1}")]
        public string Titulo { get; set; }

        [DisplayName("Autor")]
        [Required(ErrorMessage = "O campo Autor é obrigatorio.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A quantidade de caracteres no campo Autor deve ser entre {2} e {1}")]
        public string Autor { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo Descriçõa é obrigatorio.")]
        public string Descricao { get; set; }

        [DisplayName("Resumo")]
        [Required(ErrorMessage = "O campo Resumo é obrigatorio.")]
        [StringLength(1000, MinimumLength = 2, ErrorMessage = "A quantidade de caracteres no campo Resumo deve ser entre {2} e {1}")]
        public string Resumo { get; set; }

        [DisplayName("Data de Publicação")]
        [Required(ErrorMessage = "O campo Data de Publicação é obrigatorio.")]
        public DateTime DataPublicacao { get; set; }

        [DisplayName("Hora de Publicação")]
        [Required(ErrorMessage = "O campo Hora de Publicação é obrigatorio.")]
        public DateTime HoraPublicacao { get; set; }

        [DisplayName("Vísivel")]
        [Required(ErrorMessage = "O campo Vísivel é obrigatorio.")]
        public bool Visivel { get; set; }

        public List<String> Tags { get; set; }

        /*[DisplayName("Salvar")]
        public */
    }
}
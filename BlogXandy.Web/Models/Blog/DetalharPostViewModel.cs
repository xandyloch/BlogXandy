using BlogXandy.db.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogXandy.Web.Models.Blog
{
    public class DetalharPostViewModel
    {
        
        public int id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Descricao { get; set; }
        public string Resumo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public DateTime HoraPublicacao { get; set; }
        public bool Visivel { get; set; }
        public List<String> Tags { get; set; }
        public int QtdeComentarios { get; set; }

        //Cadastrar Comentario
        [DisplayName("Nome")]
        [StringLength(100,ErrorMessage = "O campo Nome deve possuir no máximo {1} caracteres!")]
        [Required(ErrorMessage ="O campo Nome é obrigatório!")]
        public String ComentarioNome { get; set; }

        [DisplayName("E-mail")]
        [StringLength(100, ErrorMessage = "O campo E-mail deve possuir no máximo {1} caracteres!")]
        [EmailAddress(ErrorMessage = "Email é inválido")]
        public String ComentarioEmail { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo Descrição é obrigatorio")]
        public String ComentarioDescricao { get; set; }

        [DisplayName("Página Web")]
        [StringLength(100, ErrorMessage = "O campo Página deve possuir no máximo {1} caracteres!")]
        public String ComentarioPaginaWeb { get; set; }

        //Listar Comentario
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public IList<Comentario> Comentario { get; set; }
    }
}
using BlogXandy.db.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogXandy.Web.Models.Blog
{
    public class ListarPostsViewModel
    {
        public List<DetalharPostViewModel> Posts { get; set; }
       // public List<String> Tags { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public string tag { get; set; }
        public List<String> Tags { get; set; }
        public string Pesquisa  { get; set; }

    }
}
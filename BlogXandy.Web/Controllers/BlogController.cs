using BlogXandy.db;
using BlogXandy.db.Classes;
using BlogXandy.Web.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogXandy.Web.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index(int? pagina, string tag, string pesquisa)// ? >> aceita parametro nulo
        {
            var paginaCorreta = pagina.GetValueOrDefault(1);
            var registroPorPagina = 10;

            var conexaoBanco = new ConexaoBanco();

            var posts = (from p in conexaoBanco.Posts
                                where p.Visivel
                                //orderby p.DataPublicacao descending
                                select p);
            if (!string.IsNullOrEmpty(tag))
            {
                posts = (from p in posts
                         where p.tagpost.Any(x => x.IdTag.ToUpper() == tag.ToUpper())
                         select p);
            }
            if (!string.IsNullOrEmpty(pesquisa))
            {
                posts = (from p in posts
                         where p.Titulo.ToUpper().Contains(pesquisa.ToUpper())
                            || p.Resumo.ToUpper().Contains(pesquisa.ToUpper())
                            || p.Descricao.ToUpper().Contains(pesquisa.ToUpper())
                         select p);
            }

            var viewModel = new ListarPostsViewModel();

            var qtdeRegistros = posts.Count();
            var indiceDaPagina = paginaCorreta - 1;
            var qtdeRegistrosPular = (indiceDaPagina * registroPorPagina);
            var qtdePaginas = Math.Ceiling((decimal)qtdeRegistros / (decimal)registroPorPagina);

            viewModel.Posts = (from p in posts orderby p.DataPublicacao descending select p).Skip(qtdeRegistrosPular).Take(registroPorPagina).ToList();

            viewModel.PaginaAtual = paginaCorreta;
            viewModel.TotalPaginas = (int)qtdePaginas;
            viewModel.tag = tag;

            viewModel.Tags = (from p in conexaoBanco.TagClass
                              where conexaoBanco.PostsTags.Any(x => x.IdTag == p.Tag)
                              orderby p.Tag
                              select p.Tag).ToList();

            viewModel.Pesquisa = pesquisa;
            //viewModel.Posts = posts;
            return View(viewModel);
        }

        public ActionResult _Paginacao()
        {
            return PartialView();
        }

        public ActionResult Post(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var posts = (from x in conexaoBanco.Posts
                         where x.Id == id
                         select x).FirstOrDefault();

            var viewModel = new DetalharPostViewModel();
            viewModel.id     = posts.Id;
            viewModel.Resumo = posts.Resumo;
            viewModel.Titulo = posts.Titulo;
            viewModel.Visivel = posts.Visivel;
            viewModel.Autor = posts.Autor;
            viewModel.DataPublicacao = posts.DataPublicacao;
            viewModel.Descricao = posts.Descricao;

            viewModel.Tags = (from p in posts.tagpost
                              select p.IdTag).ToList();

            
            /*viewModel.Tags = (from p in 
                              select p.IdTag).ToList();*/

            return View(viewModel);
        }
    }
}
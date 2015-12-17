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

            //viewModel.Posts = (from p in posts orderby p.DataPublicacao descending select p).Skip(qtdeRegistrosPular).Take(registroPorPagina).ToList();
            viewModel.Posts = (from p in posts
                               orderby p.DataPublicacao descending
                               select new DetalharPostViewModel {

                                   DataPublicacao = p.DataPublicacao,
                                   Autor = p.Autor,
                                   Descricao = p.Descricao,
                                   id = p.Id,
                                   Resumo = p.Resumo,
                                   Titulo = p.Titulo,
                                   Visivel = p.Visivel,
                                   QtdeComentarios = p.comentario.Count,

                                    }).Skip(qtdeRegistrosPular).Take(registroPorPagina).ToList();
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

        public ActionResult Post(int id, int? pagina)
        {
            var conexaoBanco = new ConexaoBanco();
            var posts = (from x in conexaoBanco.Posts
                         where x.Id == id
                         select x).FirstOrDefault();

            var viewModel = new DetalharPostViewModel();
            PreencherViewModel(posts, viewModel, pagina);


            /*viewModel.Tags = (from p in 
                              select p.IdTag).ToList();*/

            return View(viewModel);
        }

        private  void PreencherViewModel(Post posts, DetalharPostViewModel viewModel, int? pagina)
        {


            viewModel.id = posts.Id;
            viewModel.Resumo = posts.Resumo;
            viewModel.Titulo = posts.Titulo;
            viewModel.Visivel = posts.Visivel;
            viewModel.Autor = posts.Autor;
            viewModel.DataPublicacao = posts.DataPublicacao;
            viewModel.Descricao = posts.Descricao;
            viewModel.QtdeComentarios = posts.comentario.Count;
            viewModel.Tags = (from p in posts.tagpost
                              select p.IdTag).ToList();

            var paginaCorreta = pagina.GetValueOrDefault(1);
            var registroPorPagina = 10;

            var qtdeRegistros = posts.comentario.Count();
            var indiceDaPagina = paginaCorreta - 1;
            var qtdeRegistrosPular = (indiceDaPagina * registroPorPagina);
            var qtdePaginas = Math.Ceiling((decimal)qtdeRegistros / (decimal)registroPorPagina);

            viewModel.Comentario = (from p in posts.comentario
                                    orderby p.DataHora descending
                                    select p).Skip(qtdeRegistrosPular).Take(registroPorPagina).ToList();
            viewModel.PaginaAtual = paginaCorreta;
            viewModel.TotalPaginas = (int)qtdePaginas;
            
        }

        [HttpPost]
        public ActionResult Post(DetalharPostViewModel viewModel)
        {
            var conexaoBanco = new ConexaoBanco();
            var post = (from p in conexaoBanco.Posts
                        where p.Id == viewModel.id
                        select p).FirstOrDefault();
            if (ModelState.IsValid)
            {
                
                if(post == null)
                {
                    throw new Exception(string.Format("Post código {0} não encontrado.", viewModel.id));
                }

                var comentario = new Comentario();
                comentario.AdmPost = HttpContext.User.Identity.IsAuthenticated;
                comentario.Descricao = viewModel.ComentarioDescricao;
                comentario.Email = viewModel.ComentarioEmail;
                comentario.IdPost = viewModel.id;
                comentario.Nome = viewModel.ComentarioNome;
                comentario.PaginaWeb = viewModel.ComentarioPaginaWeb;
                comentario.DataHora = DateTime.Now;

                try
                {
                    conexaoBanco.Comentarios.Add(comentario);
                    conexaoBanco.SaveChanges();
                    return Redirect(Url.Action("Post", new
                    {
                        ano = post.DataPublicacao.Year,
                        mes = post.DataPublicacao.Month,
                        dia = post.DataPublicacao.Day,
                        titulo = post.Titulo,
                        id = post.Id

                    })+"#comentarios");
                }
                catch( Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }

            }
            PreencherViewModel(post, viewModel, null);
            return View(viewModel);
        }
    }
}
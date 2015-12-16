using BlogXandy.db;
using BlogXandy.db.Classes;
using BlogXandy.Web.Models.Administracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogXandy.Web.Controllers
{
    [Authorize]
    public class AdministracaoController : Controller
    {
        // GET: Administracao
          public ActionResult Index() { 
        
             return View();
        }

        public ActionResult CadastrarPost()
        {
            var viewModel = new CadastrarPostViewModel();
            viewModel.DataPublicacao = DateTime.Now;
            viewModel.HoraPublicacao = DateTime.Now;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult CadastrarPost(CadastrarPostViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {


                var conexao = new ConexaoBanco();

                var post = new Post();
                var datapub = ViewModel.DataPublicacao.Date;
                var horapub = ViewModel.HoraPublicacao;

                var datcon = new DateTime(datapub.Year,
                                 datapub.Month,
                                 datapub.Day,
                                 horapub.Hour,
                                 horapub.Minute,
                                 horapub.Second);

                post.Titulo = ViewModel.Titulo;
                post.Autor = ViewModel.Autor;
                post.DataPublicacao = datcon;
                post.Resumo = ViewModel.Resumo;
                post.Descricao = ViewModel.Descricao;
                post.Visivel = ViewModel.Visivel;

                post.tagpost = new List<PostTag>();
                if(ViewModel.Tags != null)
                {
                    foreach(var item in ViewModel.Tags)
                    {
                        var tagexiste = (from p in conexao.TagClass
                                         where p.Tag.ToLower() == item.ToLower()
                                         select p).Any();
                        if (!tagexiste)
                        {
                            var tagClass = new TagClass();
                            tagClass.Tag = item;
                            conexao.TagClass.Add(tagClass);
                        }

                        var postTag1 = new PostTag();
                        postTag1.IdTag = item;
                        post.tagpost.Add(postTag1);
                    }
                }


                conexao.Posts.Add(post);
                try
                {
                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }
                

            }

            return View(ViewModel);
        }

        public ActionResult EditarPost(int id)
        {

            var conexao = new ConexaoBanco();
            var posts = (from x in conexao.Posts
                            where x.Id == id
                         select x).FirstOrDefault();
            if(posts == null)
            {
                throw new Exception(string.Format("Post com código {0} não encontrado.", id));
            }


            var viewModel = new CadastrarPostViewModel();
            viewModel.Titulo = posts.Titulo;
            viewModel.Autor = posts.Autor;
            viewModel.Descricao = posts.Descricao;
            viewModel.DataPublicacao = posts.DataPublicacao;
            viewModel.HoraPublicacao = posts.DataPublicacao;
            viewModel.Resumo = posts.Resumo;
            viewModel.Visivel = posts.Visivel;
            viewModel.id = posts.Id;




            viewModel.Tags = (from p in posts.tagpost
                              select p.IdTag).ToList();


                        
            return View(viewModel);
                 

        }
        [HttpPost]
        public ActionResult EditarPost(CadastrarPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();
                
                var posts = conexao.Posts.Where(x => x.Id == viewModel.id).FirstOrDefault();
                if(posts != null)
                {

                    var datapub = viewModel.DataPublicacao.Date;
                    var horapub = viewModel.HoraPublicacao;

                    var datcon = new DateTime(datapub.Year,
                                     datapub.Month,
                                     datapub.Day,
                                     horapub.Hour,
                                     horapub.Minute,
                                     horapub.Second);

                    posts.Titulo = viewModel.Titulo;
                    posts.Autor = viewModel.Autor;
                    posts.DataPublicacao = datcon;
                    posts.Resumo = viewModel.Resumo;
                    posts.Descricao = viewModel.Descricao;
                    posts.Visivel = viewModel.Visivel;

                    var postsTagsAtuais = posts.tagpost.ToList();
                    foreach(var item in postsTagsAtuais)
                    {
                        conexao.PostsTags.Remove(item);
                    }
                    if (viewModel.Tags != null)
                    {
                        foreach (var item in viewModel.Tags)
                        {
                            var tagexiste = (from p in conexao.TagClass
                                             where p.Tag.ToLower() == item.ToLower()
                                             select p).Any();
                            if (!tagexiste)
                            {
                                var tagClass = new TagClass();
                                tagClass.Tag = item;
                                conexao.TagClass.Add(tagClass);
                            }

                            var postTag1 = new PostTag();
                            postTag1.IdTag = item;
                            posts.tagpost.Add(postTag1);
                        }
                    }



                    try
                    {
                        conexao.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch(Exception exp)
                    {
                        ModelState.AddModelError("", exp.Message);
                    }
                    
                    
                   
                }
            }
            
            return View(viewModel);
        }

        public ActionResult ExcluirPost(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var post = (from p in conexaoBanco.Posts
                        where p.Id == id
                        select p).FirstOrDefault();
            if (post == null)
            {
                throw new Exception(string.Format("Post código {0} não existe.", id));
            }
            conexaoBanco.Posts.Remove(post);
            conexaoBanco.SaveChanges();

            return RedirectToAction("Index", "Blog");
        }

        #region ExcluirComentario
        public ActionResult ExcluirComentario(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var comentario = (from p in conexaoBanco.Comentarios
                              where p.Id == id
                              select p).FirstOrDefault();
            if (comentario == null)
            {
                throw new Exception(string.Format("Comentário código {0} não foi encontrado.", id));
            }
            conexaoBanco.Comentarios.Remove(comentario);
            conexaoBanco.SaveChanges();

            var post = (from p in conexaoBanco.Posts
                        where p.Id == comentario.IdPost
                        select p).First();
            return Redirect(Url.Action("Post", "Blog", new
            {
                ano = post.DataPublicacao.Year,
                mes = post.DataPublicacao.Month,
                dia = post.DataPublicacao.Day,
                titulo = post.Titulo,
                id = post.Id
            }) + "#comentarios");
        }
        #endregion


    }
}
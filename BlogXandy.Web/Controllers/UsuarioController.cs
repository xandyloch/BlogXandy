using BlogXandy.db;
using BlogXandy.db.Classes;
using BlogXandy.Web.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogXandy.Web.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {//lista usuario

            var conexaoBanco = new ConexaoBanco();
            var usuarios = (from p in conexaoBanco.Usuarios
                         orderby p.Nome ascending
                         select p).ToList();
            
            return View(usuarios);
        }

        public ActionResult CadastrarUsuario()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CadastrarUsuario(CadastrarUsuarioViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {


                var conexao = new ConexaoBanco();
                var usuario = new Usuario();

                usuario.Login = ViewModel.Login;
                usuario.Nome = ViewModel.Nome;
                usuario.Senha = ViewModel.Senha;


                conexao.Usuarios.Add(usuario);
                try
                {
                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }

            }
                return View(ViewModel);
        }

        public ActionResult EditarUsuario(int id) {

            var conexao = new ConexaoBanco();
            var usuarios = (from x in conexao.Usuarios
                         where x.Id == id
                         select x).FirstOrDefault();
            if (usuarios == null)
            {
                throw new Exception(string.Format("Post com código {0} não encontrado.", id));
            }

            var viewModel = new CadastrarUsuarioViewModel();
            viewModel.Login = usuarios.Login;
            viewModel.Senha = usuarios.Senha;
            viewModel.Nome = usuarios.Nome;
            viewModel.id = usuarios.Id;


            return View( viewModel);
        }
        
        [HttpPost]
        public ActionResult EditarUsuario(CadastrarUsuarioViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();
                var usuarios = conexao.Usuarios.Where(x => x.Id == ViewModel.id).FirstOrDefault();
                if (usuarios != null)
                {

                    usuarios.Login = ViewModel.Login;
                    usuarios.Nome = ViewModel.Nome;
                    usuarios.Senha = ViewModel.Senha;

                    try
                    {
                        conexao.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception exp)
                    {
                        ModelState.AddModelError("", exp.Message);
                    }


                }


            }
            
            
            return View(ViewModel);
        }

        public ActionResult ExcluirUsuario(int id)
        {

            var conexaoBanco = new ConexaoBanco();
            var usuarios = (from p in conexaoBanco.Usuarios
                        where p.Id == id
                        select p).FirstOrDefault();
            if (usuarios == null)
            {
                throw new Exception(string.Format("Usuário {0} não existe.", id));
            }
            conexaoBanco.Usuarios.Remove(usuarios);
            conexaoBanco.SaveChanges();

            return RedirectToAction("Index", "Usuario");

        }


    }
}
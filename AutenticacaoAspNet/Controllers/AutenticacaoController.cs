using AutenticacaoAspNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutenticacaoAspNet.Util;
using System.Security.Claims;

namespace AutenticacaoAspNet.Controllers
{
    public class AutenticacaoController : Controller
    {
        private UsuariosContext db = new UsuariosContext();

        [HttpGet]
        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro (CadastroUsuarioViewModel model)
        {
            //Se os dados forem inválidos.
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            if(db.Usuarios.Count(x => x.Login == model.Login) > 0)
            {
                ModelState.AddModelError("Login", "O nome de login já existe, por gentileza, escolha outro nome.");
                return View(model);
            }

            Usuarios user = new Usuarios
            {
                Nome = model.Nome,
                Login = model.Login,
                Senha = Hash.GerarHash(model.Senha)
            };

            db.Usuarios.Add(user);
            db.SaveChanges();

            TempData["Mensagem"] = "O cadastro do usuário foi realizado com sucesso, efetue o login para continuar.";
            return RedirectToAction("Login", "Autenticacao");
        }

        public ActionResult Login (string ReturnUrl)
        {
            var viewModel = new LoginViewModel
            {
                UrlRetorno = ReturnUrl
            };

            return View(viewModel); 
        }

        [HttpPost]
        public ActionResult Login (LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = db.Usuarios.FirstOrDefault(x => x.Login == model.Login);

            //Usuário não encontrado
            if(usuario == null)
            {
                ModelState.AddModelError("Login", "O usuário não foi localizado");
                return View(model);
            }

            //Usuário válido, porém com senha incorreta.
            if(usuario.Senha != Hash.GerarHash(model.Senha))
            {
                ModelState.AddModelError("Senha", "Senha incorreta");
                return View(model);
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim("Login", usuario.Login)
            }, "ApplicationCookie");

            /*
             * Realiza o login do usuário no OWIN, passando o objeto [identity] do tipo ClaimsIdentity
             * Logo após, irá criar um cookie de autenticação na aplicação cotendo as informações deste usuário.
             */
            Request.GetOwinContext().Authentication.SignIn(identity);

            //Redireciona o usuário para a url que o mesmo estava anteriormente.
            if (!string.IsNullOrWhiteSpace(model.UrlRetorno) || Url.IsLocalUrl(model.UrlRetorno))
                return Redirect(model.UrlRetorno);

            else
                return RedirectToAction("Index", "Regras");
        }

        public ActionResult Logout ()
        {
            //Destroi o cookie que foi criado anteriormente no login do usuário.
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }
    }
}
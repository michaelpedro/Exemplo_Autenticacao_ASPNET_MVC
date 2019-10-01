using AutenticacaoAspNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutenticacaoAspNet.Util;

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

            Usuarios user = new Usuarios
            {
                Nome = model.Nome,
                Login = model.Login,
                Senha = Hash.GerarHash(model.Senha)
            };

            db.Usuarios.Add(user);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
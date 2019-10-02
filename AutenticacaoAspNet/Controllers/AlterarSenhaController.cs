using AutenticacaoAspNet.Models;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using AutenticacaoAspNet.Util;
using System.Data.Entity;

namespace AutenticacaoAspNet.Controllers
{
    public class AlterarSenhaController : Controller
    {
        UsuariosContext db = new UsuariosContext();

        [Authorize]
        [HttpGet]
        public ActionResult AlterarSenha()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var identity = User.Identity as ClaimsIdentity;

            //Obtém o login do usuário autenticado
            var login = identity.Claims.FirstOrDefault(x => x.Type == "Login").Value;

            //Obtém o usuário do banco de dados.
            var usuario = db.Usuarios.FirstOrDefault(x => x.Login == login);

            //Se a senha atual corresponde com a senha do usuário autenticado.
            if(Hash.GerarHash(model.SenhaAtual) != usuario.Senha)
            {
                ModelState.AddModelError("SenhaAtual", "Senha incorreta");
                return View();
            }

            usuario.Senha = Hash.GerarHash(model.NovaSenha);
            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
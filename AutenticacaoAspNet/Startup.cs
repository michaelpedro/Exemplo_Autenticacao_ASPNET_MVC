using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(AutenticacaoAspNet.Startup))]

namespace AutenticacaoAspNet
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Define as opções de configuração da autenticação via cookie na aplicação.
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                //Tipo de valor padrão do atributo [AuthenticationType].
                AuthenticationType = "ApplicationCookie",

                /*
                 * View de login da aplicação.
                 * Caso o usuário tente acessar alguma view restrita sem estar logado, a biblioteca [OWIN] irá automaticamente
                 * redirecionar o usuário para a rota da string abaixo.
                 */
                LoginPath = new PathString("/Autenticacao/Login")
            });
        }
    }
}

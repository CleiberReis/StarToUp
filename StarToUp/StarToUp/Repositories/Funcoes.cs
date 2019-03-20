using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using StarToUp.Models;

namespace StarToUp.Repositories
{
    public class Funcoes
    {
        public static bool AutenticarUsuario(string login, string senha)
        {
            Context _db = new Context();
            var query = (from u in _db.StartupCadastros
                         where u.Email == login &&
                         u.Senha == senha
                         select u).SingleOrDefault();
            if (query == null)
            {
                return false;
            }
            FormsAuthentication.SetAuthCookie(query.Email, false);
            //HttpContext.Current.Response.Cookies["Usuario"].Value = query.Email;
            //HttpContext.Current.Response.Cookies["Usuario"].Expires = DateTime.Now.AddDays(10);
            HttpContext.Current.Session["Usuario"] = query.Email;
            return true;
        }
        public static StartupCadastro GetUsuario()
        {
            string _login = HttpContext.Current.User.Identity.Name;
            //if (HttpContext.Current.Request.Cookies.Count > 0 || HttpContext.Current.Request.Cookies["Usuario"] != null)
            if (HttpContext.Current.Session.Count > 0 ||
           HttpContext.Current.Session["Usuario"] != null)
            {
                _login = HttpContext.Current.Session["Usuario"].ToString();
                //_login = HttpContext.Current.Request.Cookies["Usuario"].Value.ToString();
                if (_login == "")
                {
                    return null;
                }
                else
                {
                    Context _db = new Context();
                    StartupCadastro startupCadastro = (from u in _db.StartupCadastros
                                                       where u.Email == _login
                                                       select u).SingleOrDefault();
                    return startupCadastro;
                }
            }
            else
            {
                return null;
            }

        }

        public static StartupCadastro GetUsuario(string _login)
        {
            if (_login == "")
            {
                return null;
            }
            else
            {
                Context _db = new Context();
                StartupCadastro startupCadastro = (from u in _db.StartupCadastros
                                                   where u.Email == _login
                                                   select u).SingleOrDefault();
                return startupCadastro;
            }
        }

        internal static bool AutenticarUsuario(object email, object senha)
        {
            throw new NotImplementedException();
        }

        internal static bool AutenticarUsuario(string email, object senha)
        {
            throw new NotImplementedException();
        }

        public static void Deslogar()
        {
            HttpContext.Current.Session["Usuario"] = "";
            //HttpContext.Current.Response.Cookies["Usuario"].Value = "";
            FormsAuthentication.SignOut();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using StarToUp.Models;

namespace StarToUp.Repositories
{
    public class FuncoesEmpresa
    {

        public static bool AutenticarUsuarioEmpresa(string login, string senha)
        {
            Context _db = new Context();
            var query = (from e in _db.Empresas
                         where e.Email == login &&
                         e.Senha == senha
                         select e).SingleOrDefault();
            if (query == null)
            {
                return false;
            }
            FormsAuthentication.SetAuthCookie(query.Email, false);
            HttpContext.Current.Session["Usuario"] = query.Email;
            return true;
        }

        public static Empresa GetUsuarioEmpresa()
        {
            string _login = HttpContext.Current.User.Identity.Name;
            if (HttpContext.Current.Session.Count > 0 ||
           HttpContext.Current.Session["Usuario"] != null)
            {
                _login = HttpContext.Current.Session["Usuario"].ToString();
                if (_login == "")
                {
                    return null;
                }
                else
                {
                    Context _db = new Context();
                    Empresa empresaCadastro = (from e in _db.Empresas
                                                       where e.Email == _login
                                                       select e).SingleOrDefault();
                    return empresaCadastro;
                }
            }
            else
            {
                return null;
            }

        }

        internal static bool AutenticarUsuarioEmpresa(object email, object senha)
        {
            throw new NotImplementedException();
        }

        internal static bool AutenticarUsuarioEmpresa(string email, object senha)
        {
            throw new NotImplementedException();
        }

        public static void DeslogarEmpresa()
        {
            HttpContext.Current.Session["Usuario"] = "";
            FormsAuthentication.SignOut();
        }
    }
}
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
            var query = (from e in _db.EmpresaCadastros
                         where e.Email == login &&
                         e.Senha == senha
                         select e).SingleOrDefault();
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

        public static EmpresaCadastro GetUsuarioEmpresa()
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
                    EmpresaCadastro empresaCadastro = (from e in _db.EmpresaCadastros
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
            //HttpContext.Current.Response.Cookies["Usuario"].Value = "";
            FormsAuthentication.SignOut();
        }
    }
}
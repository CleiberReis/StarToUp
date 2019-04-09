using StarToUp.Models;
using StarToUp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StarToUp.Controllers
{
    public class LogonEmpresaController : Controller
    {
        // GET: LogonEmpresa
        // GET: Logon
        public ActionResult Logar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logar(string email, string senha)
        {
            if (FuncoesEmpresa.AutenticarUsuarioEmpresa(email, senha) == false)
            {
                ViewBag.Error = "Nome de usuário e/ou senha inválida";
                return View();
            }
            return RedirectToAction("Index", "EmpresaCadastros");
        }

        public ActionResult AcessoNegadoEmpresa()
        {
            using (Context c = new Context())
            {
                return View();
            }
        }

        public ActionResult Logoff()
        {
            StarToUp.Repositories.Funcoes.Deslogar();
            return RedirectToAction("Index", "Principal");
        }    }
}

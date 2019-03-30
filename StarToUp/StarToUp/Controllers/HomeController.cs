using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StarToUp.Models;
using StarToUp.Repositories;

namespace StarToUp.Controllers
{
    public class HomeController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            //if (Session["Usuario"] != null)
            //{
            //    Funcoes.GetUsuario();
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Logar", "Logon");
            //}
            IEnumerable<Evento> eventos = db.Eventoes.ToList();
            ViewBag.Eventos = eventos;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(StartupCadastro u)
        //{
        //    // esta action trata o post (login)
        //    if (ModelState.IsValid) //verifica se é válido
        //    {
        //        using (StartupCadastrosController dc = new StartupCadastrosController())
        //        {
        //            var v = dc.StartupCadastros.Where(a => a.NomeUsuario.Equals(u.Nome) && a.Senha.Equals(u.Senha)).FirstOrDefault();
        //            if (v != null)
        //            {
        //                Session["StartupCadastroID"] = v.Id.ToString();
        //                Session["nome"] = v.NomeUsuario.ToString();
        //                return RedirectToAction("Index");
        //            }
        //        }
        //    }
        //    return View(u);
        //}

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

    }
}
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
            IEnumerable<StartupCadastro> startupCadastros = db.StartupCadastros.ToList();
            IEnumerable<Evento> eventos = db.Eventoes.ToList();
            ViewBag.StartupCadastros = startupCadastros;
            ViewBag.Eventos = eventos;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Principal()
        {
            IEnumerable<StartupCadastro> startupCadastros = db.StartupCadastros.ToList();
            IEnumerable<Evento> eventos = db.Eventoes.ToList();
            ViewBag.StartupCadastros = startupCadastros;
            ViewBag.Eventos = eventos;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Principal([Bind(Include = "CadastroID,Nome,Email,Assunto,Mensagem")] Contato contato)
        {
            if (ModelState.IsValid)
            {
                db.Contatos.Add(contato);
                db.SaveChanges();

                GmailEmailService gmail = new GmailEmailService();
                EmailMessage msg = new EmailMessage();
                msg.Body = "<!DOCTYPE HTML><html><body><p>Mensagem de: <b>" + contato.Nome + "</b><br /><br /> " + contato.Mensagem + "</p><p>Contato do remetente para retorno: " + contato.Email + "</p></body></html>";
                msg.IsHtml = true;
                msg.Subject = contato.Assunto;
                msg.ToEmail = "startoupcontact@gmail.com";
                gmail.SendEmailMessage(msg);

                return RedirectToAction("Principal", "Home");
            }

            return View(contato);
        }

    }
}
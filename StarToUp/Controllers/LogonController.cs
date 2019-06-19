using Newtonsoft.Json;
using StarToUp.Models;
using StarToUp.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StarToUp.Controllers
{
    public class LogonController : Controller
    {
        private Context db = new Context();

        // GET: Logon
        public ActionResult Logar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logar(string email, string senha)
        {
            if (Funcoes.AutenticarUsuario(email, senha) == false)
            {
                ViewBag.Error = "Nome de usuário e/ou senha inválida";
                return View();
            }

            return RedirectToAction("Blog", "Startups");
        }

        public ActionResult AcessoNegado()
        {
            using (Context c = new Context())
            {
                return View();
            }
        }

        public ActionResult Logoff()
        {
            StarToUp.Repositories.Funcoes.Deslogar();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EsqueciSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EsqueciSenha([Bind(Include = "StartupID,Nome,Email,Senha,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Sobre,Objetivo,DataFundacao,TamanhoTime,Logotipo,ImgStartup1,ImgStartup2,ImagemMVP1,ImagemMVP2,Hash,SegmentacaoID,AvaliacaoID")] Startup startup,
            HttpPostedFileBase logoTipo, HttpPostedFileBase imgStartup1, HttpPostedFileBase imgStartup2, HttpPostedFileBase imagemMVP1, HttpPostedFileBase imagemMVP2)
        {
            Startup s = db.Startups.Where(e => e.Email == startup.Email).ToList().SingleOrDefault();

            string hash = (s.Email + s.Nome + s.Bairro);
            s.Hash = hash;

            ((IObjectContextAdapter)db).ObjectContext.Detach(s);
            db.Entry(s).State = EntityState.Modified;
            db.SaveChanges();

            GmailEmailService gmail = new GmailEmailService();
            EmailMessage msg = new EmailMessage();
            msg.Body = "<!DOCTYPE HTML><html><body><p>Olá!</p><p>Clique no link abaixo para redefinir senha:<br/><a href= http://localhost:50072/Logon/ValidarHash?h=" + hash + ">Redefinir Senha</a></p><p>Aconselhamos que por segurança você altere sua senha para uma mais forte!</p><p>Atenciosamente,<br/>StarToUp.</p></body></html>";
            msg.IsHtml = true;
            msg.Subject = "Redefinir Senha - StarToUp";
            msg.ToEmail = startup.Email;
            gmail.SendEmailMessage(msg);

            return View();

        }

        public ActionResult ValidarHash(string h)
        {
            if (h == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Startup s = db.Startups.Where(e => e.Hash == h).ToList().SingleOrDefault();
            int id = s.StartupID;
            Startup startup = db.Startups.Find(id);
            if (startup == null)
            {
                return HttpNotFound();
            }
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", startup.SegmentacaoID);
            return View(startup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidarHash([Bind(Include = "StartupID,Nome,Email,Senha,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Sobre,Objetivo,DataFundacao,TamanhoTime,Logotipo,ImgStartup1,ImgStartup2,ImagemMVP1,ImagemMVP2,Hash,SegmentacaoID,AvaliacaoID")] Startup startup,
            HttpPostedFileBase logoTipo, HttpPostedFileBase imgStartup1, HttpPostedFileBase imgStartup2, HttpPostedFileBase imagemMVP1, HttpPostedFileBase imagemMVP2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(startup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Logar");
            }
            ViewBag.Message = "Link inválido, entre em contato com a StarToUp";
            return View();

        }
    }
}
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
    public class LogonEmpresaController : Controller
    {
        private Context db = new Context();
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
            return RedirectToAction("Blog", "Empresas");
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
            return RedirectToAction("Index", "Home");
        }        public ActionResult EsqueciSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EsqueciSenha([Bind(Include = "EmpresaID,Nome,Email,Senha,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Logotipo,Sobre,RazaoSocial,LinkInstagram,LinkFacebook,LinkLinkedin,TermoUso,Hash,SegmentacaoID")] Empresa empresa,
            HttpPostedFileBase logoTipo)
        {
            Empresa e = db.Empresas.Where(s => s.Email == empresa.Email).ToList().SingleOrDefault();

            string hash = (e.Email + e.Numero);
            e.Hash = hash;

            ((IObjectContextAdapter)db).ObjectContext.Detach(e);
            db.Entry(e).State = EntityState.Modified;
            db.SaveChanges();

            GmailEmailService gmail = new GmailEmailService();
            EmailMessage msg = new EmailMessage();
            msg.Body = "<!DOCTYPE HTML><html><body><p>Olá!</p><p>Clique no link abaixo para redefinir senha:<br/><a href= http://localhost:50072/LogonEmpresa/ValidarHash?h=" + hash + ">Redefinir Senha</a></p><p>Aconselhamos que por segurança você altere sua senha para uma mais forte!</p><p>Atenciosamente,<br/>StarToUp.</p></body></html>";
            msg.IsHtml = true;
            msg.Subject = "Redefinir Senha - StarToUp";
            msg.ToEmail = empresa.Email;
            gmail.SendEmailMessage(msg);

            return View();

        }

        public ActionResult ValidarHash(string h)
        {
            if (h == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa s = db.Empresas.Where(e => e.Hash == h).ToList().SingleOrDefault();
            int id = s.EmpresaID;
            Empresa empresa = db.Empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", empresa.SegmentacaoID);
            return View(empresa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidarHash([Bind(Include = "EmpresaID,Nome,Email,Senha,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Logotipo,Sobre,RazaoSocial,LinkInstagram,LinkFacebook,LinkLinkedin,TermoUso,Hash,SegmentacaoID")] Empresa empresa, HttpPostedFileBase logomarca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Logar");
            }
            ViewBag.Message = "Link inválido, entre em contato com a StarToUp";
            return View();

        }    }
}

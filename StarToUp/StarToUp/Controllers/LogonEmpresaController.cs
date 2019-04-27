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
        }        public ActionResult EsqueciSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EsqueciSenha([Bind(Include = "StartupCadastroID,Nome,Email,Senha,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Sobre,Objetivo,DataFundacao,TamanhoTime,Logotipo,ImagemLocal1,ImagemLocal2,ImagemMVP1,ImagemMVP2,ImagemMVP3,ImagemMVP4,Hash,SegmentacaoID")] StartupCadastro empresaCadastro,
            HttpPostedFileBase logoTipo, HttpPostedFileBase imagemLocal1, HttpPostedFileBase imagemLocal2, HttpPostedFileBase imagemMVP1, HttpPostedFileBase imagemMVP2, HttpPostedFileBase imagemMVP3, HttpPostedFileBase imagemMVP4)
        {
            EmpresaCadastro e = db.EmpresaCadastros.Where(s => s.Email == empresaCadastro.Email).ToList().SingleOrDefault();

            string hash = (e.Email + e.Nome + e.Bairro);
            e.Hash = hash;

            ((IObjectContextAdapter)db).ObjectContext.Detach(e);
            db.Entry(e).State = EntityState.Modified;
            db.SaveChanges();

            GmailEmailService gmail = new GmailEmailService();
            EmailMessage msg = new EmailMessage();
            msg.Body = "<!DOCTYPE HTML><html><body><p>Olá!</p><p>Clique no link abaixo para redefinir senha:<br/><a href= http://localhost:50072/LogonEmpresa/ValidarHash/" + (e.EmpresaCadastroID) + ">Redefinir Senha</a></p><p>Aconselhamos que por segurança você altere sua senha para uma mais forte!</p><p>Atenciosamente,<br/>StarToUp.</p></body></html>";
            msg.IsHtml = true;
            msg.Subject = "Redefinir Senha - StarToUp";
            msg.ToEmail = empresaCadastro.Email;
            gmail.SendEmailMessage(msg);

            return View();

        }

        public ActionResult ValidarHash(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpresaCadastro empresaCadastro = db.EmpresaCadastros.Find(id);
            if (empresaCadastro == null)
            {
                return HttpNotFound();
            }
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", empresaCadastro.SegmentacaoID);
            return View(empresaCadastro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidarHash([Bind(Include = "EmpresaCadastroID,Nome,Email,Senha,RazaoSocial,QtdFuncionario,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Logomarca,Objetivo,Hash,SegmentacaoID")] EmpresaCadastro empresaCadastro, HttpPostedFileBase logomarca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresaCadastro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Logar");
            }
            ViewBag.Message = "Link inválido, entre em contato com a StarToUp";
            return View();

        }    }
}

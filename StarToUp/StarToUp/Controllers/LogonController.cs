﻿using Newtonsoft.Json;
using StarToUp.Models;
using StarToUp.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

            return RedirectToAction("Index", "StartupCadastros");
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
            return RedirectToAction("Index", "Principal");
        }

        public ActionResult EsqueciSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EsqueciSenha([Bind(Include = "StartupCadastroID,Nome,Email,Senha,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Sobre,Objetivo,DataFundacao,TamanhoTime,Logotipo,ImagemLocal1,ImagemLocal2,ImagemMVP1,ImagemMVP2,ImagemMVP3,ImagemMVP4,Hash,SegmentacaoID")] StartupCadastro startupCadastro,
            HttpPostedFileBase logoTipo, HttpPostedFileBase imagemLocal1, HttpPostedFileBase imagemLocal2, HttpPostedFileBase imagemMVP1, HttpPostedFileBase imagemMVP2, HttpPostedFileBase imagemMVP3, HttpPostedFileBase imagemMVP4)
        {
            StartupCadastro s = db.StartupCadastros.Where(e => e.Email == startupCadastro.Email).ToList().SingleOrDefault();

            string hash = (startupCadastro.Email + startupCadastro.DataFundacao + startupCadastro.Cep);
            s.Email = startupCadastro.Email;
            s.Hash = hash;

            db.Entry(startupCadastro).State = EntityState.Modified;
            db.SaveChanges();

            GmailEmailService gmail = new GmailEmailService();
            EmailMessage msg = new EmailMessage();
            msg.Body = "<!DOCTYPE HTML><html><body><p>Olá!</p><p>Clique no link abaixo para redefinir senha: <br/><a href = http://localhost:50072/Logon/ValidarHash?"+ hash + ">" +"</p><p>Aconselhamos que por segurança você altere sua senha para uma mais forte!</p><p>Atenciosamente,<br/>StarToUp.</p></body></html>";
            msg.IsHtml = true;
            msg.Subject = "Redefinir Senha - StarToUp";
            msg.ToEmail = startupCadastro.Email;
            gmail.SendEmailMessage(msg);

            return View();

        }

        public ActionResult ValidarHash()
        {
            return View();
            //return View();
        }
    }
}
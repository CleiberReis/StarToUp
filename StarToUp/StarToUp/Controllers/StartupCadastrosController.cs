﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using StarToUp.Models;
using StarToUp.Repositories;

namespace StarToUp.Controllers
{
    public class StartupCadastrosController : Controller
    {
        private Context db = new Context();

        // GET: StartupCadastros
        public ActionResult Index()
        {
            if (Session["Usuario"] != null)
            {
                Funcoes.GetUsuario();

                var startupCadastros = db.StartupCadastros.Include(s => s.Segmentacoes);
                startupCadastros = db.StartupCadastros.OrderByDescending(x => x.DataCadastro);
                IEnumerable<EmpresaCadastro> empresaCadastros = db.EmpresaCadastros.ToList();
                ViewBag.EmpresaCadastros = empresaCadastros;
                IEnumerable<StartupCadastro> startupCadastro = db.StartupCadastros.ToList();
                ViewBag.StartupCadastros = startupCadastro;
                return View(startupCadastros.ToList());
            }
            else
            {
                return RedirectToAction("Logar", "Logon");
            }

        }

        public ActionResult IndexStartups()
        {
            var startupCadastros = db.StartupCadastros.Include(s => s.Segmentacoes);
            return View(startupCadastros.ToList());
        }

        public ActionResult Ranking()
        {
            var avaliacao = db.StartupCadastros.Include(s => s.Segmentacoes).Include(a => a.Avaliacoes).OrderByDescending(m => m.AvaliacaoID).Take(50);
            return View(avaliacao.ToList());
        }

        [HttpPost]
        public ActionResult SearchRanking(FormCollection fc, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var startups = db.StartupCadastros.Include(s => s.Segmentacoes).Include(a => a.Avaliacoes).Where(s => s.Segmentacoes.Descricao.Contains(searchString)).OrderByDescending(o => o.AvaliacaoID);
                return View("Ranking", startups.ToList());
            }
            else
            {
                return RedirectToAction("Ranking");
            }
        }


        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Logoff()
        {
            StarToUp.Repositories.Funcoes.Deslogar();
            return RedirectToAction("Principal", "Home");
        }

        // GET: StartupCadastros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StartupCadastro startupCadastro = db.StartupCadastros.Find(id);
            if (startupCadastro == null)
            {
                return HttpNotFound();
            }
            List<Avaliacao> avaliacoes = db.Avaliacoes.OfType<Avaliacao>().OrderBy(c => c.AvaliacaoID).ToList();
            ViewBag.Avaliacoes = avaliacoes;
            ViewBag.StartupCadastro = id;
            ViewBag.StartupCadastroID = new SelectList(db.StartupCadastros, "StartupCadastroID", "Nome", startupCadastro.StartupCadastroID);
            return View(startupCadastro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "StartupID,Nome,Email,Senha,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Sobre,DataFundacao,Logotipo,ImgStartup1,ImgStartup2,ImagemMVP1,ImagemMVP2,LinkInstagram,LinkFacebook,LinkLinkedin,TermoUso,Hash,SegmentacaoID,AvaliacaoID")] StartupCadastro startupCadastro,
            HttpPostedFileBase logoTipo, HttpPostedFileBase imgStartup1, HttpPostedFileBase imgStartup2, HttpPostedFileBase imagemMVP1, HttpPostedFileBase imagemMVP2, string[] selectedAvaliacao, int StartupID, Avaliacao avaliacao)
        {
            StartupCadastro s = db.StartupCadastros.Where(e => e.StartupCadastroID == StartupID).ToList().SingleOrDefault();

            foreach (var item in selectedAvaliacao)
            {
                int idAvaliacao = int.Parse(item);
                int valor1 = s.AvaliacaoID;
                int valor2 = valor1 + idAvaliacao;
                int media = (valor2 / valor2) * idAvaliacao;
                s.AvaliacaoID = media;
                ((IObjectContextAdapter)db).ObjectContext.Detach(s);
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public ActionResult Create()
        {
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao");
            return View();
        }

        // POST: StartupCadastros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StartupCadastroID,Nome,Email,Senha,DataCadastro,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Sobre,Objetivo,DataFundacao,TamanhoTime,Logotipo,ImagemLocal1,ImagemLocal2,ImagemMVP1,ImagemMVP2,ImagemMVP3,ImagemMVP4,LinkInstagram,LinkFacebook,LinkLinkedin,TermoUso,Hash,SegmentacaoID")] StartupCadastro startupCadastro,
            HttpPostedFileBase logoTipo, HttpPostedFileBase imagemLocal1, HttpPostedFileBase imagemLocal2, HttpPostedFileBase imagemMVP1, HttpPostedFileBase imagemMVP2, HttpPostedFileBase imagemMVP3, HttpPostedFileBase imagemMVP4)
        {
            ViewBag.FotoMensagem = "";
            try
            {
                if (ModelState.IsValid && startupCadastro.TermoUso == true)
                {
                    string fileName = "";
                    string contentType = "";
                    string path = "";

                    if (logoTipo != null && logoTipo.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(logoTipo.FileName);
                        contentType = logoTipo.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        logoTipo.SaveAs(path);
                        startupCadastro.Logotipo = fileName;
                    }

                    if (imagemLocal1 != null && imagemLocal1.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemLocal1.FileName);
                        contentType = imagemLocal1.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemLocal1.SaveAs(path);
                        startupCadastro.ImagemLocal1 = fileName;
                    }

                    if (imagemLocal2 != null && imagemLocal2.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemLocal2.FileName);
                        contentType = imagemLocal2.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemLocal2.SaveAs(path);
                        startupCadastro.ImagemLocal2 = fileName;
                    }

                    if (imagemMVP1 != null && imagemMVP1.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemMVP1.FileName);
                        contentType = imagemMVP1.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemMVP1.SaveAs(path);
                        startupCadastro.ImagemMVP1 = fileName;
                    }

                    if (imagemMVP2 != null && imagemMVP2.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemMVP2.FileName);
                        contentType = imagemMVP2.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemMVP2.SaveAs(path);
                        startupCadastro.ImagemMVP2 = fileName;
                    }

                    if (imagemMVP3 != null && imagemMVP3.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemMVP3.FileName);
                        contentType = imagemMVP3.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemMVP3.SaveAs(path);
                        startupCadastro.ImagemMVP3 = fileName;
                    }

                    if (imagemMVP4 != null && imagemMVP4.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemMVP4.FileName);
                        contentType = imagemMVP4.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemMVP4.SaveAs(path);
                        startupCadastro.ImagemMVP4 = fileName;
                    }

                    startupCadastro.DataCadastro = DateTime.Now;
                    db.StartupCadastros.Add(startupCadastro);
                    db.SaveChanges();

                    GmailEmailService gmail = new GmailEmailService();
                    EmailMessage msg = new EmailMessage();
                    msg.Body = "<!DOCTYPE HTML><html><body><p>" + startupCadastro.Nome + ",<br/>Seja bem-vinda(o)!</p><p>Sua decolagem está prestes a iniciar!<br/>Clique no link abaixo para finalizar seu cadastro:</p><a href= http://localhost:50072/Logon/Logar/" + "> Faça seu login aqui!</a><p>Esperamos que você decole com a gente!</p><p>Atenciosamente,<br/>StarToUp.</p></body></html>";
                    msg.IsHtml = true;
                    msg.Subject = "E-mail de Confirmação - StarToUp";
                    msg.ToEmail = startupCadastro.Email;
                    gmail.SendEmailMessage(msg);

                    var response = Request["g-recaptcha-response"];
                    //chave secreta que foi gerada no site
                    const string secret = "6Ldjv5gUAAAAAE8AgNayyITU99Lexs-BEeZU4imx";
                    var client = new WebClient();
                    var reply =
                    client.DownloadString(

                   string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                   secret, response));
                    var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
                    //Response false – devemos ver qual a mensagem de erro
                    if (!captchaResponse.Success)
                    {
                        if (captchaResponse.ErrorCodes.Count <= 0) return View();
                        var error = captchaResponse.ErrorCodes[0].ToLower();
                        switch (error)
                        {
                            case ("missing-input-secret"):
                                ViewBag.Message = "The secret parameter is missing.";
                                break;
                            case ("invalid-input-secret"):
                                ViewBag.Message = "The secret parameter is invalid or malformed.";
                                break;
                            case ("missing-input-response"):
                                ViewBag.Message = "The response parameter is missing.";
                                break;
                            case ("invalid-input-response"):
                                ViewBag.Message = "The response parameter is invalid or malformed.";
                                break;
                            default:
                                ViewBag.Message = "Error occured. Please try again";
                                break;
                        }
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = "Valid";
                        return RedirectToAction("../Home/Principal");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.FotoMensagem = "Não foi possível salvar a foto";
            }
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", startupCadastro.SegmentacaoID);
            ViewBag.StartupCadastroID = new SelectList(db.StartupCadastros, "StartupCadastroID", "Nome", startupCadastro.StartupCadastroID);
            return View(startupCadastro);
        }

        // GET: StartupCadastros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StartupCadastro startupCadastro = db.StartupCadastros.Find(id);
            if (startupCadastro == null)
            {
                return HttpNotFound();
            }
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", startupCadastro.SegmentacaoID);
            return View(startupCadastro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StartupCadastroID,Nome,Email,Senha,DataCadastro,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Sobre,Objetivo,DataFundacao,TamanhoTime,Logotipo,ImagemLocal1,ImagemLocal2,ImagemMVP1,ImagemMVP2,ImagemMVP3,ImagemMVP4,LinkInstagram,LinkFacebook,LinkLinkedin,TermoUso,Hash,SegmentacaoID")] StartupCadastro startupCadastro,
            HttpPostedFileBase logoTipo, HttpPostedFileBase imagemLocal1, HttpPostedFileBase imagemLocal2, HttpPostedFileBase imagemMVP1, HttpPostedFileBase imagemMVP2, HttpPostedFileBase imagemMVP3, HttpPostedFileBase imagemMVP4)
        {
            ViewBag.FotoMensagem = "";
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = "";
                    string contentType = "";
                    string path = "";

                    StartupCadastro startupCadastroBD = db.StartupCadastros.Find(startupCadastro.StartupCadastroID);
                    if (logoTipo != null && logoTipo.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(logoTipo.FileName);
                        contentType = logoTipo.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        logoTipo.SaveAs(path);
                        startupCadastro.Logotipo = fileName;
                    }
                    else
                    {
                        if (logoTipo == null)
                        {
                            if (startupCadastroBD.Logotipo != null)
                            {
                                if (startupCadastroBD.Logotipo.Length > 0)
                                {
                                    //usa valores que ja estao no BD
                                    startupCadastro.Logotipo = startupCadastroBD.Logotipo;
                                }
                            }
                        }
                    }

                    if (imagemLocal1 != null && imagemLocal1.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemLocal1.FileName);
                        contentType = imagemLocal1.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemLocal1.SaveAs(path);
                        startupCadastro.ImagemLocal1 = fileName;
                    }
                    else
                    {
                        if (imagemLocal1 == null)
                        {
                            if (startupCadastroBD.ImagemLocal1 != null)
                            {
                                if (startupCadastroBD.ImagemLocal1.Length > 0)
                                {
                                    //usa valores que ja estao no BD
                                    startupCadastro.ImagemLocal1 = startupCadastroBD.ImagemLocal1;
                                }
                            }
                        }
                    }

                    if (imagemLocal2 != null && imagemLocal2.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemLocal2.FileName);
                        contentType = imagemLocal2.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemLocal2.SaveAs(path);
                        startupCadastro.ImagemLocal2 = fileName;
                    }
                    else
                    {
                        if (imagemLocal2 == null)
                        {
                            if (startupCadastroBD.ImagemLocal2 != null)
                            {
                                if (startupCadastroBD.ImagemLocal2.Length > 0)
                                {
                                    //usa valores que ja estao no BD
                                    startupCadastro.ImagemLocal2 = startupCadastroBD.ImagemLocal2;
                                }
                            }
                        }
                    }

                    if (imagemMVP1 != null && imagemMVP1.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemMVP1.FileName);
                        contentType = imagemMVP1.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemMVP1.SaveAs(path);
                        startupCadastro.ImagemMVP1 = fileName;
                    }
                    else
                    {
                        if (imagemMVP1 == null)
                        {
                            if (startupCadastroBD.ImagemMVP1 != null)
                            {
                                if (startupCadastroBD.ImagemMVP1.Length > 0)
                                {
                                    //usa valores que ja estao no BD
                                    startupCadastro.ImagemMVP1 = startupCadastroBD.ImagemMVP1;
                                }
                            }
                        }
                    }

                    if (imagemMVP2 != null && imagemMVP2.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemMVP2.FileName);
                        contentType = imagemMVP2.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemMVP2.SaveAs(path);
                        startupCadastro.ImagemMVP2 = fileName;
                    }
                    else
                    {
                        if (imagemMVP2 == null)
                        {
                            if (startupCadastroBD.ImagemMVP2 != null)
                            {
                                if (startupCadastroBD.ImagemMVP2.Length > 0)
                                {
                                    //usa valores que ja estao no BD
                                    startupCadastro.ImagemMVP2 = startupCadastroBD.ImagemMVP2;
                                }
                            }
                        }
                    }

                    if (imagemMVP3 != null && imagemMVP3.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemMVP3.FileName);
                        contentType = imagemMVP3.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemMVP3.SaveAs(path);
                        startupCadastro.ImagemMVP3 = fileName;
                    }
                    else
                    {
                        if (imagemMVP3 == null)
                        {
                            if (startupCadastroBD.ImagemMVP3 != null)
                            {
                                if (startupCadastroBD.ImagemMVP3.Length > 0)
                                {
                                    //usa valores que ja estao no BD
                                    startupCadastro.ImagemMVP3 = startupCadastroBD.ImagemMVP3;
                                }
                            }
                        }
                    }

                    if (imagemMVP4 != null && imagemMVP4.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemMVP4.FileName);
                        contentType = imagemMVP4.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemMVP4.SaveAs(path);
                        startupCadastro.ImagemMVP4 = fileName;
                    }
                    else
                    {
                        if (imagemMVP4 == null)
                        {
                            if (startupCadastroBD.ImagemMVP4 != null)
                            {
                                if (startupCadastroBD.ImagemMVP4.Length > 0)
                                {
                                    //usa valores que ja estao no BD
                                    startupCadastro.ImagemMVP4 = startupCadastroBD.ImagemMVP4;
                                }
                            }
                        }
                    }

                    ((IObjectContextAdapter)db).ObjectContext.Detach(startupCadastroBD);
                    db.Entry(startupCadastro).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.FotoMensagem = "Não foi possível salvar a foto";
            }
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", startupCadastro.SegmentacaoID);
            return View(startupCadastro);
        }

        [HttpPost]
        public ActionResult Search(FormCollection fc, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var startupCadastros = db.StartupCadastros.Include(s => s.Segmentacoes).Where(s => s.Segmentacoes.Descricao.Contains(searchString)).OrderBy(o => o.Segmentacoes.Descricao);
                return View("IndexStartups", startupCadastros.ToList());
            }
            else
            {
                return RedirectToAction("IndexStartups");
            }
        }

        public ActionResult SearchStartup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchStartup(FormCollection fc, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var startupCadastros = db.StartupCadastros.Include(s => s.Segmentacoes).Where(s => s.Segmentacoes.Descricao.Contains(searchString) || s.Nome.Contains(searchString) || s.Objetivo.Contains(searchString) || s.Sobre.Contains(searchString) || s.Cep.Contains(searchString)).OrderBy(o => o.DataCadastro);
                return View("SearchStartup", startupCadastros.ToList());
            }
            else
            {
                return RedirectToAction("SearchStartup");
            }
        }



        // GET: StartupCadastros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StartupCadastro startupCadastro = db.StartupCadastros.Find(id);
            if (startupCadastro == null)
            {
                return HttpNotFound();
            }
            return View(startupCadastro);
        }

        // POST: StartupCadastros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StartupCadastro startupCadastro = db.StartupCadastros.Find(id);
            db.StartupCadastros.Remove(startupCadastro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

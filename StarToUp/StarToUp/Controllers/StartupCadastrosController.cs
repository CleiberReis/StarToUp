using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
            //if (Session["Usuario"] != null)
            //{
            //    Funcoes.GetUsuario();
            //    var startupCadastros = db.StartupCadastros.Include(s => s.Segmentacoes);
            //    return View(startupCadastros.ToList());
            //}
            //else
            //{
            //    return RedirectToAction("Logar", "Logon");
            //}

            var startupCadastros = db.StartupCadastros.Include(s => s.Segmentacoes);
            return View(startupCadastros.ToList());
        }


        //public ActionResult Login()
        //{
        //    return View();
        //}

        //public ActionResult Logoff()
        //{
        //    StarToUp.Repositories.Funcoes.Deslogar();
        //    return RedirectToAction("Index", "Home");
        //}

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
        //                return RedirectToAction("Index", "IndexStartup");
        //            }
        //        }
        //    }
        //    return View(u);
        //}

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
            return View(startupCadastro);
        }

        // GET: StartupCadastros/Create
        public ActionResult Create()
        {
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao");
            return View();
        }

        // POST: StartupCadastros/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StartupCadastroID,Nome,Email,Senha,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Sobre,Objetivo,DataFundacao,TamanhoTime,Logotipo,ImagemLocal1,ImagemLocal2,ImagemMVP1,ImagemMVP2,ImagemMVP3,ImagemMVP4,SegmentacaoID")] StartupCadastro startupCadastro,
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
                    db.StartupCadastros.Add(startupCadastro);
                    db.SaveChanges();

                    GmailEmailService gmail = new GmailEmailService();
                    EmailMessage msg = new EmailMessage();
                    msg.Body = "<!DOCTYPE HTML><html><body><p>" + startupCadastro.Nome + ",<br/>Seja bem-vinda(o)!</p><p>Sua decolagem está prestes a iniciar!<br/>Clique no link abaixo para finalizar seu cadastro:</p><a href= http://localhost:50072/Logon/Logar/" + "> Faça seu login aqui!</a><p>Esperamos que você decole com a gente!</p><p>Atenciosamente,<br/>StarToUp.</p></body></html>";
                    msg.IsHtml = true;
                    msg.Subject = "E-mail de Confirmação - StarToUp";
                    msg.ToEmail = startupCadastro.Email;
                    gmail.SendEmailMessage(msg);

                    return RedirectToAction("../Home/Index");
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

        // POST: StartupCadastros/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StartupCadastroID,Nome,Email,Senha,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Sobre,Objetivo,DataFundacao,TamanhoTime,Logotipo,ImagemLocal1,ImagemLocal2,ImagemMVP1,ImagemMVP2,ImagemMVP3,ImagemMVP4,SegmentacaoID")] StartupCadastro startupCadastro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(startupCadastro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", startupCadastro.SegmentacaoID);
            return View(startupCadastro);
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

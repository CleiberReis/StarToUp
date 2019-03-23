using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
            var startupCadastros = db.StartupCadastros.Include(s => s.TipoUsuario);
            return View(startupCadastros.ToList());
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
            return View(startupCadastro);
        }

        // GET: StartupCadastros/Create
        public ActionResult Create()
        {
            ViewBag.TipoUsuarioID = new SelectList(db.TipoUsuarios, "TipoUsuarioID", "Descricao");
            return View();
        }

        // POST: StartupCadastros/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StartupCadastroID,Nome,Email,Senha,TipoUsuarioID")] StartupCadastro startupCadastro)
        {
            if (ModelState.IsValid)
            {
                db.StartupCadastros.Add(startupCadastro);
                db.SaveChanges();
                Session["StartupCadastroID"] = startupCadastro;

                GmailEmailService gmail = new GmailEmailService();
                EmailMessage msg = new EmailMessage();
                msg.Body = "<!DOCTYPE HTML><html><body><p>" + startupCadastro.Nome + ",<br/>Seja bem-vinda(o)!</p><p>Sua decolagem está prestes a iniciar!<br/>Clique no link abaixo para finalizar seu cadastro:</p><a href= http://localhost:50072/Logon/Logar/ " + "> Faça seu login aqui!</a><p>Esperamos que você decole com a gente!</p><p>Atenciosamente,<br/>StarToUp.</p></body></html>";
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
                    return RedirectToAction("../Home/Index");
                }

            }

            ViewBag.TipoUsuarioID = new SelectList(db.TipoUsuarios, "TipoUsuarioID", "Descricao", startupCadastro.TipoUsuarioID);
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
            ViewBag.TipoUsuarioID = new SelectList(db.TipoUsuarios, "TipoUsuarioID", "Descricao", startupCadastro.TipoUsuarioID);
            return View(startupCadastro);
        }

        // POST: StartupCadastros/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StartupCadastroID,Nome,Email,Senha,TipoUsuarioID")] StartupCadastro startupCadastro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(startupCadastro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoUsuarioID = new SelectList(db.TipoUsuarios, "TipoUsuarioID", "Descricao", startupCadastro.TipoUsuarioID);
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

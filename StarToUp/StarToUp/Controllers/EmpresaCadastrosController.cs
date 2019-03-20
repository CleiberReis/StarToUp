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
    public class EmpresaCadastrosController : Controller
    {
        private Context db = new Context();

        // GET: EmpresaCadastros
        public ActionResult Index()
        {
            var empresaCadastros = db.EmpresaCadastros.Include(e => e.TipoUsuario);
            return View(empresaCadastros.ToList());
        }

        // GET: EmpresaCadastros/Details/5
        public ActionResult Details(int? id)
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
            return View(empresaCadastro);
        }

        // GET: EmpresaCadastros/Create
        public ActionResult Create()
        {
            ViewBag.TipoUsuarioID = new SelectList(db.TipoUsuarios, "TipoUsuarioID", "Descricao");
            return View();
        }

        // POST: EmpresaCadastros/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpresaCadastroID,Nome,Email,Senha,TipoUsuarioID")] EmpresaCadastro empresaCadastro)
        {
            if (ModelState.IsValid)
            {
                db.EmpresaCadastros.Add(empresaCadastro);
                db.SaveChanges();
                Session["EmpresaCadastroID"] = empresaCadastro;

                //GmailEmailService gmail = new GmailEmailService();
                //EmailMessage msg = new EmailMessage();
                //msg.Body = "<!DOCTYPE HTML><html><body><p>Cara Empresa,<br/>Seja bem-vinda!</p><p>Seu conhecimento das Startups que estão decolando está prestes a iniciar!<br/>Clique no link abaixo para finalizar seu cadastro:</p><p>'LINK'</p><p>Esperamos que você decole com a gente!</p><p>Atenciosamente,<br/>StarToUp.</p></body></html>";
                //msg.IsHtml = true;
                //msg.Subject = "E-mail de Confirmação - StarToUp";
                //msg.ToEmail = empresaCadastro.Email;
                //gmail.SendEmailMessage(msg);

                
              
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

            ViewBag.TipoUsuarioID = new SelectList(db.TipoUsuarios, "TipoUsuarioID", "Descricao", empresaCadastro.TipoUsuarioID);
            return View(empresaCadastro);
        }

        // GET: EmpresaCadastros/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.TipoUsuarioID = new SelectList(db.TipoUsuarios, "TipoUsuarioID", "Descricao", empresaCadastro.TipoUsuarioID);
            return View(empresaCadastro);
        }

        // POST: EmpresaCadastros/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpresaCadastroID,Nome,Email,Senha,TipoUsuarioID")] EmpresaCadastro empresaCadastro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresaCadastro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoUsuarioID = new SelectList(db.TipoUsuarios, "TipoUsuarioID", "Descricao", empresaCadastro.TipoUsuarioID);
            return View(empresaCadastro);
        }

        // GET: EmpresaCadastros/Delete/5
        public ActionResult Delete(int? id)
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
            return View(empresaCadastro);
        }

        // POST: EmpresaCadastros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmpresaCadastro empresaCadastro = db.EmpresaCadastros.Find(id);
            db.EmpresaCadastros.Remove(empresaCadastro);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
            if (Session["Usuario"] != null)
            {
                FuncoesEmpresa.GetUsuarioEmpresa();
                var empresaCadastros = db.EmpresaCadastros.Include(e => e.Segmentacoes);
                return View(empresaCadastros.ToList());
            }
            else
            {
                return RedirectToAction("Logar", "LogonEmpresa");
            }

            //var empresaCadastros = db.EmpresaCadastros.Include(e => e.Segmentacoes);
            //return View(empresaCadastros.ToList());
        }

        public ActionResult IndexEmpresas()
        {
            var empresaCadastros = db.EmpresaCadastros.Include(e => e.Segmentacoes);
            return View(empresaCadastros.ToList());
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
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao");
            return View();
        }

        // POST: EmpresaCadastros/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpresaCadastroID,Nome,Email,Senha,RazaoSocial,QtdFuncionario,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Logomarca,Objetivo,SegmentacaoID")] EmpresaCadastro empresaCadastro, HttpPostedFileBase logomarca)
        {

            ViewBag.FotoMensagem = "";
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = "";
                    string contentType = "";
                    string path = "";
                    if (logomarca != null && logomarca.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(logomarca.FileName);
                        contentType = logomarca.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilEmpresa\\" + fileName;
                        logomarca.SaveAs(path);
                        empresaCadastro.Logomarca = fileName;
                    }

                    db.EmpresaCadastros.Add(empresaCadastro);
                    db.SaveChanges();

                    GmailEmailService gmail = new GmailEmailService();
                    EmailMessage msg = new EmailMessage();
                    msg.Body = "<!DOCTYPE HTML><html><body><p>" + empresaCadastro.Nome + ",<br/>Seja bem-vinda(o)!</p><p>Sua decolagem está prestes a iniciar!<br/>Clique no link abaixo para finalizar seu cadastro:</p><a href= http://localhost:50072/LogonEmpresa/Logar/" + "> Faça seu login aqui!</a><p>Esperamos que você decole com a gente!</p><p>Atenciosamente,<br/>StarToUp.</p></body></html>";
                    msg.IsHtml = true;
                    msg.Subject = "E-mail de Confirmação - StarToUp";
                    msg.ToEmail = empresaCadastro.Email;
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
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", empresaCadastro.SegmentacaoID);
            ViewBag.StartupCadastroID = new SelectList(db.StartupCadastros, "StartupCadastroID", "Nome", empresaCadastro.EmpresaCadastroID);
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
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", empresaCadastro.SegmentacaoID);
            return View(empresaCadastro);
        }

        // POST: EmpresaCadastros/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpresaCadastroID,Nome,Email,Senha,RazaoSocial,QtdFuncionario,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Logomarca,Objetivo,SegmentacaoID")] EmpresaCadastro empresaCadastro, HttpPostedFileBase logomarca)
        {
            ViewBag.FotoMensagem = "";
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = "";
                    string contentType = "";
                    string path = "";

                    EmpresaCadastro empresaCadastroBD = db.EmpresaCadastros.Find(empresaCadastro.EmpresaCadastroID);
                    if (logomarca != null && logomarca.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(logomarca.FileName);
                        contentType = logomarca.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilEmpresa\\" + fileName;
                        logomarca.SaveAs(path);
                        empresaCadastro.Logomarca = fileName;
                    }
                    else
                    {
                        if (logomarca == null)
                        {
                            if (empresaCadastroBD.Logomarca != null)
                            {
                                if (empresaCadastroBD.Logomarca.Length > 0)
                                {
                                    //usa valores que ja estao no BD
                                    empresaCadastro.Logomarca = empresaCadastroBD.Logomarca;
                                }
                            }
                        }
                    }

                    ((IObjectContextAdapter)db).ObjectContext.Detach(empresaCadastroBD);
                    db.Entry(empresaCadastro).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.FotoMensagem = "Não foi possível salvar a foto";
            }
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", empresaCadastro.SegmentacaoID);
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

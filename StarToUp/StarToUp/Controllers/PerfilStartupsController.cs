using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StarToUp.Models;

namespace StarToUp.Controllers
{
    public class PerfilStartupsController : Controller
    {
        private Context db = new Context();

        // GET: PerfilStartups
        public ActionResult Index()
        {
            var perfilStartups = db.PerfilStartups.Include(p => p.Segmentacoes).Include(p => p.StartupCadastro);
            return View(perfilStartups.ToList());
        }

        // GET: PerfilStartups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerfilStartup perfilStartup = db.PerfilStartups.Find(id);
            if (perfilStartup == null)
            {
                return HttpNotFound();
            }
            return View(perfilStartup);
        }

        // GET: PerfilStartups/Create
        public ActionResult Create()
        {
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao");
            ViewBag.StartupCadastroID = new SelectList(db.StartupCadastros, "StartupCadastroID", "Nome");
            return View();
        }

        // POST: PerfilStartups/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PerfilStartupID,NomeStartup,DataFundacao,TamanhoTime,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Sobre,Objetivo,Logotipo,ImagemLocal1,ImagemLocal2,ImagemMVP1,ImagemMVP2,ImagemMVP3,ImagemMVP4,StartupCadastroID,SegmentacaoID")] PerfilStartup perfilStartup,
            HttpPostedFileBase logoTipo, HttpPostedFileBase imagemLocal1, HttpPostedFileBase imagemLocal2, HttpPostedFileBase mvp1, HttpPostedFileBase mvp2, HttpPostedFileBase mvp3, HttpPostedFileBase mvp4)
        {
            ViewBag.LogoMensagem = "";
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
                        path =
                        System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" +
                        fileName;
                        logoTipo.SaveAs(path);
                        perfilStartup.Logotipo = fileName;
                    }
                    if (imagemLocal1 != null && imagemLocal1.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemLocal1.FileName);
                        contentType = imagemLocal1.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemLocal1.SaveAs(path);
                        perfilStartup.ImagemLocal1 = fileName;
                    }
                    if (imagemLocal2 != null && imagemLocal2.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemLocal2.FileName);
                        contentType = imagemLocal2.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemLocal2.SaveAs(path);
                        perfilStartup.ImagemLocal2 = fileName;
                    }
                    if (mvp1 != null && mvp1.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(mvp1.FileName);
                        contentType = mvp1.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        mvp1.SaveAs(path);
                        perfilStartup.ImagemMVP1 = fileName;
                    }
                    if (mvp2 != null && mvp2.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(mvp2.FileName);
                        contentType = mvp2.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        mvp2.SaveAs(path);
                        perfilStartup.ImagemMVP2 = fileName;
                    }
                    if (mvp3 != null && mvp3.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(mvp3.FileName);
                        contentType = mvp3.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        mvp3.SaveAs(path);
                        perfilStartup.ImagemMVP3 = fileName;
                    }
                    if (mvp4 != null && mvp4.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(mvp4.FileName);
                        contentType = mvp4.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        mvp4.SaveAs(path);
                        perfilStartup.ImagemMVP4 = fileName;
                    }
                    db.PerfilStartups.Add(perfilStartup);
                    db.SaveChanges();
                    return RedirectToAction("../Home/Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.LogoMensagem = "Não foi possível salvar a foto";
            }
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", perfilStartup.SegmentacaoID);
            ViewBag.StartupCadastroID = new SelectList(db.StartupCadastros, "StartupCadastroID", "Nome", perfilStartup.StartupCadastroID);
            return View(perfilStartup);
        }

        // GET: PerfilStartups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerfilStartup perfilStartup = db.PerfilStartups.Find(id);
            if (perfilStartup == null)
            {
                return HttpNotFound();
            }
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", perfilStartup.SegmentacaoID);
            ViewBag.StartupCadastroID = new SelectList(db.StartupCadastros, "StartupCadastroID", "Nome", perfilStartup.StartupCadastroID);
            return View(perfilStartup);
        }

        // POST: PerfilStartups/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PerfilStartupID,NomeStartup,DataFundacao,TamanhoTime,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Sobre,Objetivo,Logotipo,ImagemLocal1,ImagemLocal2,ImagemMVP1,ImagemMVP2,ImagemMVP3,ImagemMVP4,StartupCadastroID,SegmentacaoID")] PerfilStartup perfilStartup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(perfilStartup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", perfilStartup.SegmentacaoID);
            ViewBag.StartupCadastroID = new SelectList(db.StartupCadastros, "StartupCadastroID", "Nome", perfilStartup.StartupCadastroID);
            return View(perfilStartup);
        }

        // GET: PerfilStartups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerfilStartup perfilStartup = db.PerfilStartups.Find(id);
            if (perfilStartup == null)
            {
                return HttpNotFound();
            }
            return View(perfilStartup);
        }

        // POST: PerfilStartups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PerfilStartup perfilStartup = db.PerfilStartups.Find(id);
            db.PerfilStartups.Remove(perfilStartup);
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

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
    public class AdministradorsController : Controller
    {
        private Context db = new Context();

        // GET: Administradors
        public ActionResult Index()
        {
            IEnumerable<StartupCadastro> startupCadastros = db.StartupCadastros.ToList();
            return View(db.Administradors.ToList());
        }

        // GET: Administradors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrador administrador = db.Administradors.Find(id);
            if (administrador == null)
            {
                return HttpNotFound();
            }
            return View(administrador);
        }

        // GET: Administradors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administradors/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdministradorID,FotoAdmin,NomeAdmin,Funcao,Login,Senha")] Administrador administrador, HttpPostedFileBase fotoAdmin)
        {
            ViewBag.FotoMensagem = "";
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = "";
                    string contentType = "";
                    string path = "";
                    if (fotoAdmin != null && fotoAdmin.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(fotoAdmin.FileName);
                        contentType = fotoAdmin.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\Admin\\" + fileName;
                        fotoAdmin.SaveAs(path);
                        administrador.FotoAdmin = fileName;
                    }
                    db.Administradors.Add(administrador);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ViewBag.FotoMensagem = "Não foi possível salvar a foto";
            }
            ViewBag.AdministradorID = new SelectList(db.Administradors, "AdministradorID", "NomeAdmin", administrador.AdministradorID);

            return View(administrador);
        }

        // GET: Administradors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrador administrador = db.Administradors.Find(id);
            if (administrador == null)
            {
                return HttpNotFound();
            }
            return View(administrador);
        }

        // POST: Administradors/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdministradorID,FotoAdmin,NomeAdmin,Funcao,Login,Senha")] Administrador administrador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administrador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(administrador);
        }

        // GET: Administradors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrador administrador = db.Administradors.Find(id);
            if (administrador == null)
            {
                return HttpNotFound();
            }
            return View(administrador);
        }

        // POST: Administradors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Administrador administrador = db.Administradors.Find(id);
            db.Administradors.Remove(administrador);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Estatisticas()
        {

            return View();
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

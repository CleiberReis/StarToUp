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
                return RedirectToAction("../Home/Index");
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

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
    public class EmpresaCadastrosController : Controller
    {
        private Context db = new Context();

        // GET: EmpresaCadastros
        public ActionResult Index()
        {
            var empresaCadastro = db.EmpresaCadastro.Include(e => e.TipoUsuario);
            return View(empresaCadastro.ToList());
        }

        // GET: EmpresaCadastros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpresaCadastro empresaCadastro = db.EmpresaCadastro.Find(id);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpresaCadastroID,Nome,Email,Senha,TipoUsuarioID")] EmpresaCadastro empresaCadastro)
        {
            if (ModelState.IsValid)
            {
                db.EmpresaCadastro.Add(empresaCadastro);
                db.SaveChanges();
                return RedirectToAction("Index");
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
            EmpresaCadastro empresaCadastro = db.EmpresaCadastro.Find(id);
            if (empresaCadastro == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoUsuarioID = new SelectList(db.TipoUsuarios, "TipoUsuarioID", "Descricao", empresaCadastro.TipoUsuarioID);
            return View(empresaCadastro);
        }

        // POST: EmpresaCadastros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
            EmpresaCadastro empresaCadastro = db.EmpresaCadastro.Find(id);
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
            EmpresaCadastro empresaCadastro = db.EmpresaCadastro.Find(id);
            db.EmpresaCadastro.Remove(empresaCadastro);
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

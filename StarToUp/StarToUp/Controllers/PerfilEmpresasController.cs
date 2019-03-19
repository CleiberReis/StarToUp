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
    public class PerfilEmpresasController : Controller
    {
        private Context db = new Context();

        // GET: PerfilEmpresas
        public ActionResult Index()
        {
            var perfilEmpresas = db.PerfilEmpresas.Include(p => p.EmpresaCadastro);
            return View(perfilEmpresas.ToList());
        }

        // GET: PerfilEmpresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerfilEmpresa perfilEmpresa = db.PerfilEmpresas.Find(id);
            if (perfilEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(perfilEmpresa);
        }

        // GET: PerfilEmpresas/Create
        public ActionResult Create()
        {
            ViewBag.EmpresaCadastroID = new SelectList(db.EmpresaCadastros, "EmpresaCadastroID", "Nome");
            return View();
        }

        // POST: PerfilEmpresas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PerfilEmpresaID,NomeFantasia,RazaoSocial,SegmentoMercado,QtdFuncionario,Rua,Bairro,Numero,Complemento,Cidade,Estado,Logomarca,Objetivo,EmpresaCadastroID")] PerfilEmpresa perfilEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.PerfilEmpresas.Add(perfilEmpresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpresaCadastroID = new SelectList(db.EmpresaCadastros, "EmpresaCadastroID", "Nome", perfilEmpresa.EmpresaCadastroID);
            return View(perfilEmpresa);
        }

        // GET: PerfilEmpresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerfilEmpresa perfilEmpresa = db.PerfilEmpresas.Find(id);
            if (perfilEmpresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpresaCadastroID = new SelectList(db.EmpresaCadastros, "EmpresaCadastroID", "Nome", perfilEmpresa.EmpresaCadastroID);
            return View(perfilEmpresa);
        }

        // POST: PerfilEmpresas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PerfilEmpresaID,NomeFantasia,RazaoSocial,SegmentoMercado,QtdFuncionario,Rua,Bairro,Numero,Complemento,Cidade,Estado,Logomarca,Objetivo,EmpresaCadastroID")] PerfilEmpresa perfilEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(perfilEmpresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpresaCadastroID = new SelectList(db.EmpresaCadastros, "EmpresaCadastroID", "Nome", perfilEmpresa.EmpresaCadastroID);
            return View(perfilEmpresa);
        }

        // GET: PerfilEmpresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerfilEmpresa perfilEmpresa = db.PerfilEmpresas.Find(id);
            if (perfilEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(perfilEmpresa);
        }

        // POST: PerfilEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PerfilEmpresa perfilEmpresa = db.PerfilEmpresas.Find(id);
            db.PerfilEmpresas.Remove(perfilEmpresa);
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

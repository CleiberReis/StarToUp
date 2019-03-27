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
            var perfilStartups = db.PerfilStartups.Include(p => p.Fundadores).Include(p => p.Segmentacoes).Include(p => p.StartupCadastro);
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
            ViewBag.FundadorID = new SelectList(db.Fundadores, "FundadorID", "Nome");
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao");
            ViewBag.StartupCadastroID = new SelectList(db.StartupCadastros, "StartupCadastroID", "Nome");
            return View();
        }

        // POST: PerfilStartups/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PerfilStartupID,NomeStartup,DataFundacao,TamanhoTime,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Sobre,Objetivo,Logotipo,ImagemLocal1,ImagemLocal2,ImagemMVP1,ImagemMVP2,ImagemMVP3,ImagemMVP4,StartupCadastroID,SegmentacaoID,FundadorID")] PerfilStartup perfilStartup)
        {
            if (ModelState.IsValid)
            {
                db.PerfilStartups.Add(perfilStartup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FundadorID = new SelectList(db.Fundadores, "FundadorID", "Nome", perfilStartup.FundadorID);
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
            ViewBag.FundadorID = new SelectList(db.Fundadores, "FundadorID", "Nome", perfilStartup.FundadorID);
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", perfilStartup.SegmentacaoID);
            ViewBag.StartupCadastroID = new SelectList(db.StartupCadastros, "StartupCadastroID", "Nome", perfilStartup.StartupCadastroID);
            return View(perfilStartup);
        }

        // POST: PerfilStartups/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PerfilStartupID,NomeStartup,DataFundacao,TamanhoTime,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Sobre,Objetivo,Logotipo,ImagemLocal1,ImagemLocal2,ImagemMVP1,ImagemMVP2,ImagemMVP3,ImagemMVP4,StartupCadastroID,SegmentacaoID,FundadorID")] PerfilStartup perfilStartup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(perfilStartup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FundadorID = new SelectList(db.Fundadores, "FundadorID", "Nome", perfilStartup.FundadorID);
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

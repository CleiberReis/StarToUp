using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StarToUp.Models;

namespace StarToUp.Controllers
{
    public class EventosController : Controller
    {
        private Context db = new Context();

        // GET: Eventos
        public ActionResult Index()
        {
            return View(db.Eventoes.ToList());
        }

        // GET: Eventos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Eventoes.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // GET: Eventos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Eventos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventoID,Nome,Descricao,Foto,DataEvento")] Evento evento, HttpPostedFileBase foto)
        {
            ViewBag.FotoMensagem = "";
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = "";
                    string contentType = "";
                    string path = "";
                    if (foto != null && foto.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(foto.FileName);
                        contentType = foto.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\Evento\\" + fileName;
                        foto.SaveAs(path);
                        evento.Foto = fileName;
                    }

                    db.Eventoes.Add(evento);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.FotoMensagem = "Não foi possível salvar a foto";
            }
            ViewBag.EventoID = new SelectList(db.Eventoes, "EventoID", "Nome", evento.EventoID);
            return View(evento);
        }

        // GET: Eventos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Eventoes.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventoID,Nome,Descricao,Foto,DataEvento")] Evento evento, HttpPostedFileBase foto)
        {
            ViewBag.FotoMensagem = "";
            try
            {
                string fileName = "";
                string contentType = "";
                string path = "";
                Evento eventoBD = db.Eventoes.Find(evento.EventoID);
                if (foto != null && foto.ContentLength > 0)
                {
                    fileName = System.IO.Path.GetFileName(foto.FileName);
                    contentType = foto.ContentType;
                    path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] +
                    "\\Evento\\" + fileName;
                    foto.SaveAs(path);
                    evento.Foto = fileName;
                }
                else
                {
                    if (foto == null)
                    {
                        if (eventoBD.Foto != null)
                        {
                            if (eventoBD.Foto.Length > 0)
                            {
                                //usa valores que ja estao no BD
                                evento.Foto = eventoBD.Foto;
                            }
                        }
                    }
                }
                ((IObjectContextAdapter)db).ObjectContext.Detach(eventoBD);
                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.FotoMensagem = "Não foi possível salvar a foto";
            }
            ViewBag.CidadeID = new SelectList(db.Eventoes, "EventoID", "Nome", evento.EventoID);
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Eventoes.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // Delete FOTO
        public ActionResult DeleteFile(int? id, string arquivo)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Eventoes.Find(id);
            try
            {
                if (ModelState.IsValid)
                {
                    string file = "";
                    switch (arquivo)
                    {
                        case "Foto":
                            file =
                            System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\Evento\\" + evento.Foto;
                            break;
                    }
                    if (System.IO.File.Exists(file))
                    {
                        System.IO.File.Delete(file);
                        Evento eventoBD = db.Eventoes.Find(evento.EventoID);
                        switch (arquivo)
                        {
                            case "Foto":
                                evento.Foto = string.Empty;
                                break;
                        }
                    ((IObjectContextAdapter)db).ObjectContext.Detach(eventoBD);
                        db.Entry(evento).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.EventoID = new SelectList(db.Eventoes, "EventoID", "Nome",
                        evento.EventoID);
                        return View("Edit", evento);
                    }
                    else
                    {
                        ViewBag.FotoMensagem = "Não foi possível excluir a foto";
                    }
                }
                else
                {
                    ViewBag.FotoMensagem = "Não foi possível excluir a foto";
                }
            }
            catch (Exception ex)
            {
                ViewBag.FotoMensagem = "Não foi possível excluir a foto";
            }
            ViewBag.EventoID = new SelectList(db.Eventoes, "EventoID", "Nome", evento.EventoID);
            return View("Edit", evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evento evento = db.Eventoes.Find(id);
            db.Eventoes.Remove(evento);
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

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
        public ActionResult Create([Bind(Include = "PerfilEmpresaID,NomeFantasia,RazaoSocial,SegmentoMercado,QtdFuncionario,Rua,Bairro,Numero,Complemento,Cidade,Estado,Logomarca,Objetivo,EmpresaCadastroID")] PerfilEmpresa perfilEmpresa, HttpPostedFileBase logomarca)
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
                        perfilEmpresa.Logomarca = fileName;
                    }


                    db.PerfilEmpresas.Add(perfilEmpresa);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.FotoMensagem = "Não foi possível salvar a foto";
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
        public ActionResult Edit([Bind(Include = "PerfilEmpresaID,NomeFantasia,RazaoSocial,SegmentoMercado,QtdFuncionario,Rua,Bairro,Numero,Complemento,Cidade,Estado,Logomarca,Objetivo,EmpresaCadastroID")] PerfilEmpresa perfilEmpresa, HttpPostedFileBase logomarca)
        {
            ViewBag.FotoMensagem = "";
            try
            {
                string fileName = "";
                string contentType = "";
                string path = "";

                PerfilEmpresa perfilempresaBD = db.PerfilEmpresas.Find(perfilEmpresa.PerfilEmpresaID);
                if (logomarca != null && logomarca.ContentLength > 0)
                {
                    fileName = System.IO.Path.GetFileName(logomarca.FileName);
                    contentType = logomarca.ContentType;
                    path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilEmpresa\\" + fileName;
                    logomarca.SaveAs(path);
                    perfilEmpresa.Logomarca = fileName;
                }
                else
                {
                    if (logomarca == null)
                    {
                        if (perfilempresaBD.Logomarca != null)
                        {
                            if (perfilempresaBD.Logomarca.Length > 0)
                            {
                                //usa valores que ja estao no BD
                                perfilEmpresa.Logomarca = perfilempresaBD.Logomarca;
                            }
                        }
                    }
                }
            ((IObjectContextAdapter)db).ObjectContext.Detach(perfilempresaBD);
                db.Entry(perfilEmpresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.LogomarcaMensagem = "Não foi possível salvar a foto";
            }
            ViewBag.EmpresaCadastroID = new SelectList(db.EmpresaCadastros, "EmpresaCadastroID", "Nome", perfilEmpresa.EmpresaCadastroID);
            return View(perfilEmpresa);
        }

        // DELETEFILE - Foto do perfil da empresa

        public ActionResult DeleteFile(int? id, string arquivo)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerfilEmpresa perfilEmpresa = db.PerfilEmpresas.Find(id);
            try
            {
                if (ModelState.IsValid)
                {
                    string file = "";
                    switch (arquivo)
                    {
                        case "Logomarca":
                            file =
                            System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilEmpresa\\" + perfilEmpresa.Logomarca;
                            break;
                    }
                    if (System.IO.File.Exists(file))
                    {
                        System.IO.File.Delete(file);
                        PerfilEmpresa perfilEmpresaBD = db.PerfilEmpresas.Find(perfilEmpresa.PerfilEmpresaID);
                        switch (arquivo)
                        {
                            case "Logomarca":
                                perfilEmpresa.Logomarca = string.Empty;
                                break;
                        }
                    ((IObjectContextAdapter)db).ObjectContext.Detach(perfilEmpresaBD);
                        db.Entry(perfilEmpresa).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.PerfilEmpresaID = new SelectList(db.PerfilEmpresas, "PerfilEmpresaID", "Nome", perfilEmpresa.PerfilEmpresaID);
                        return View("Edit", perfilEmpresa);
                    }
                    else
                    {
                        ViewBag.LogomarcaMensagem = "Não foi possível excluir a foto";
                    }
                }
                else
                {
                    ViewBag.LogomarcaMensagem = "Não foi possível excluir a foto";
                }
            }
            catch (Exception ex)
            {
                ViewBag.LogomarcaMensagem = "Não foi possível excluir a foto";
            }
            ViewBag.PerfilEmpresaID = new SelectList(db.PerfilEmpresas, "PerfilEmpresaID", "Nome", perfilEmpresa.PerfilEmpresaID);
            return View("Edit", perfilEmpresa);
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

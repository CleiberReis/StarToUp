using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StarToUp.Models;
using StarToUp.Repositories;

namespace StarToUp.Controllers
{
    public class PerfilStartupsController : Controller
    {
        private Context db = new Context();

        // GET: PerfilStartups
        public ActionResult Index()
        {
            if (Session["Usuario"] != null)
            {
                Funcoes.GetUsuario();
                var perfilStartups = db.PerfilStartups.Include(p => p.Segmentacoes).Include(p => p.StartupCadastro);
                return View(perfilStartups.ToList());
            }
            else
            {
                return RedirectToAction("Logar", "Logon");
            }

        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logoff()
        {
            StarToUp.Repositories.Funcoes.Deslogar();
            return RedirectToAction("Index", "Home");
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(StartupCadastro u)
        //{
        //    // esta action trata o post (login)
        //    if (ModelState.IsValid) //verifica se é válido
        //    {
        //        using (StartupCadastrosController dc = new StartupCadastrosController())
        //        {
        //            var v = dc.StartupCadastros.Where(a => a.NomeUsuario.Equals(u.Nome) && a.Senha.Equals(u.Senha)).FirstOrDefault();
        //            if (v != null)
        //            {
        //                Session["StartupCadastroID"] = v.Id.ToString();
        //                Session["nome"] = v.NomeUsuario.ToString();
        //                return RedirectToAction("Index", "IndexStartup");
        //            }
        //        }
        //    }
        //    return View(u);
        //}

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}



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
            HttpPostedFileBase logoTipo, HttpPostedFileBase imagemLocal1, HttpPostedFileBase imagemLocal2, HttpPostedFileBase imagemMVP1, HttpPostedFileBase imagemMVP2, HttpPostedFileBase imagemMVP3, HttpPostedFileBase imagemMVP4)
        {
            ViewBag.FotoMensagem = "";
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
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
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

                    if (imagemMVP1 != null && imagemMVP1.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemMVP1.FileName);
                        contentType = imagemMVP1.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemMVP1.SaveAs(path);
                        perfilStartup.ImagemMVP1 = fileName;
                    }

                    if (imagemMVP2 != null && imagemMVP2.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemMVP2.FileName);
                        contentType = imagemMVP2.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemMVP2.SaveAs(path);
                        perfilStartup.ImagemMVP2 = fileName;
                    }

                    if (imagemMVP3 != null && imagemMVP3.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemMVP3.FileName);
                        contentType = imagemMVP3.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemMVP3.SaveAs(path);
                        perfilStartup.ImagemMVP3 = fileName;
                    }

                    if (imagemMVP4 != null && imagemMVP4.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemMVP4.FileName);
                        contentType = imagemMVP4.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        imagemMVP4.SaveAs(path);
                        perfilStartup.ImagemMVP4 = fileName;
                    }
                    db.PerfilStartups.Add(perfilStartup);
                    db.SaveChanges();
                    return RedirectToAction("../Home/Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.FotoMensagem = "Não foi possível salvar a foto";
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

        [HttpPost]
        public ActionResult DeleteAJAX(string perfilStartupID)
        {
            int perfilIdInt;
            if (int.TryParse(perfilStartupID, out perfilIdInt))
            {
                PerfilStartup estado = db.PerfilStartups.Find(perfilIdInt);
                db.PerfilStartups.Remove(estado);
                db.SaveChanges();
                return Json(true);
                //return RedirectToAction("Index");
            }
            return Json(false);
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

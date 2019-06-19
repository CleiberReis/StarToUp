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
using StarToUp.Repositories;

namespace StarToUp.Controllers
{
    public class StartupsController : Controller
    {
        private Context db = new Context();

        // GET: Startups
        public ActionResult Index()
        {
            var startups = db.Startups.Include(s => s.Avaliacoes).Include(s => s.Segmentacoes);
            return View(startups.ToList());
        }

        public ActionResult Blog()
        {
            if (Session["Usuario"] != null)
            {
                Funcoes.GetUsuario();

                var startups = db.Startups.Include(s => s.Segmentacoes);
                startups = db.Startups.OrderByDescending(x => x.StartupID);
                IEnumerable<Empresa> empresas = db.Empresas.ToList();
                ViewBag.Empresas = empresas;
                IEnumerable<Startup> startup = db.Startups.ToList();
                ViewBag.Startups = startup;

                return View(startups.ToList());
            }
            else
            {
                return RedirectToAction("Logar", "Logon");
            }
        }

        public ActionResult Logoff()
        {
            StarToUp.Repositories.Funcoes.Deslogar();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Empresas()
        {
            var empresas = db.Empresas.Include(e => e.Segmentacoes);
            return View(empresas.ToList());
        }

        public ActionResult Ranking()
        {
            var avaliacao = db.Startups.Include(s => s.Segmentacoes).Include(a => a.Avaliacoes).OrderByDescending(m => m.AvaliacaoID).Take(50);
            return View(avaliacao.ToList());
        }

        [HttpPost]
        public ActionResult SearchRanking(FormCollection fc, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var startups = db.Startups.Include(s => s.Segmentacoes).Include(a => a.Avaliacoes).Where(s => s.Segmentacoes.Descricao.Contains(searchString)).OrderByDescending(o => o.AvaliacaoID);
                return View("Ranking", startups.ToList());
            }
            else
            {
                return RedirectToAction("Ranking");
            }
        }

        public ActionResult Vagas()
        {
            return View(db.Empregos.ToList());
        }

        public ActionResult CriarVagas()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarVagas([Bind(Include = "EmpregoID,TituloVaga,LocalVaga,SalarioVaga,RequisitosVaga,DescricaoVaga")] Emprego emprego)
        {
            if (ModelState.IsValid)
            {
                db.Empregos.Add(emprego);
                db.SaveChanges();
                return RedirectToAction("Vagas");
            }

            return View(emprego);
        }

        public ActionResult EditarVagas(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprego emprego = db.Empregos.Find(id);
            if (emprego == null)
            {
                return HttpNotFound();
            }
            return View(emprego);
        }

        public ActionResult DetalheVagas(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprego emprego = db.Empregos.Find(id);
            if (emprego == null)
            {
                return HttpNotFound();
            }
            return View(emprego);
        }

        // POST: Empregos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarVagas([Bind(Include = "EmpregoID,TituloVaga,LocalVaga,SalarioVaga,RequisitosVaga,DescricaoVaga")] Emprego emprego)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emprego).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emprego);
        }

        // GET: Startups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Startup startup = db.Startups.Find(id);
            if (startup == null)
            {
                return HttpNotFound();
            }
            List<Avaliacao> avaliacoes = db.Avaliacoes.OfType<Avaliacao>().OrderBy(c => c.AvaliacaoID).ToList();
            ViewBag.Avaliacoes = avaliacoes;
            ViewBag.Startup = id;
            ViewBag.StartupID = new SelectList(db.Startups, "StartupID", "Nome", startup.StartupID);
            return View(startup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "StartupID,Nome,Email,Senha,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Sobre,DataFundacao,Logotipo,ImgStartup1,ImgStartup2,ImagemMVP1,ImagemMVP2,LinkInstagram,LinkFacebook,LinkLinkedin,TermoUso,Hash,SegmentacaoID,AvaliacaoID")] Startup startup,
            HttpPostedFileBase logoTipo, HttpPostedFileBase imgStartup1, HttpPostedFileBase imgStartup2, HttpPostedFileBase imagemMVP1, HttpPostedFileBase imagemMVP2, string[] selectedAvaliacao, int StartupID, Avaliacao avaliacao)
        {
            Startup s = db.Startups.Where(e => e.StartupID == StartupID).ToList().SingleOrDefault();

            foreach (var item in selectedAvaliacao)
            {
                int idAvaliacao = int.Parse(item);
                int valor1 = s.AvaliacaoID;
                int valor2 = valor1 + idAvaliacao;
                int media = (valor2 / valor2) * idAvaliacao;
                s.AvaliacaoID = media;
                ((IObjectContextAdapter)db).ObjectContext.Detach(s);
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Startups/Create
        public ActionResult Create()
        {
            ViewBag.AvaliacaoID = new SelectList(db.Avaliacoes, "AvaliacaoID", "Descricao");
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao");
            return View();
        }

        // POST: Startups/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StartupID,Nome,Email,Senha,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Sobre,DataFundacao,Logotipo,ImgStartup1,ImgStartup2,ImagemMVP1,ImagemMVP2,LinkInstagram,LinkFacebook,LinkLinkedin,TermoUso,Hash,SegmentacaoID,AvaliacaoID")] Startup startup,
            HttpPostedFileBase logoTipo, HttpPostedFileBase imgStartup1, HttpPostedFileBase imgStartup2, HttpPostedFileBase imagemMVP1, HttpPostedFileBase imagemMVP2)
        {
            ViewBag.FotoMensagem = "";
            try
            {
                if (ModelState.IsValid && startup.TermoUso == true)
                {
                    string fileName = "";
                    string contentType = "";
                    string path = "";

                    if (logoTipo != null && logoTipo.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(logoTipo.FileName);
                        contentType = logoTipo.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\Startup\\" + fileName;
                        logoTipo.SaveAs(path);
                        startup.Logotipo = fileName;
                    }

                    if (imgStartup1 != null && imgStartup1.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imgStartup1.FileName);
                        contentType = imgStartup1.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\Startup\\" + fileName;
                        imgStartup1.SaveAs(path);
                        startup.ImgStartup1 = fileName;
                    }

                    if (imgStartup2 != null && imgStartup2.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imgStartup2.FileName);
                        contentType = imgStartup2.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\Startup\\" + fileName;
                        imgStartup2.SaveAs(path);
                        startup.ImgStartup2 = fileName;
                    }

                    if (imagemMVP1 != null && imagemMVP1.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemMVP1.FileName);
                        contentType = imagemMVP1.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\Startup\\" + fileName;
                        imagemMVP1.SaveAs(path);
                        startup.ImagemMVP1 = fileName;
                    }

                    if (imagemMVP2 != null && imagemMVP2.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemMVP2.FileName);
                        contentType = imagemMVP2.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\Startup\\" + fileName;
                        imagemMVP2.SaveAs(path);
                        startup.ImagemMVP2 = fileName;
                    }

                    startup.AvaliacaoID = 3;
                    db.Startups.Add(startup);
                    db.SaveChanges();

                    GmailEmailService gmail = new GmailEmailService();
                    EmailMessage msg = new EmailMessage();
                    msg.Body = "<!DOCTYPE HTML><html><body><p>" + startup.Nome + ",<br/>Seja bem-vinda(o)!</p><p>Sua decolagem está prestes a iniciar!<br/>Clique no link abaixo para finalizar seu cadastro:</p><a href= http://localhost:50072/Logon/Logar/" + "> Faça seu login aqui!</a><p>Esperamos que você decole com a gente!</p><p>Atenciosamente,<br/>StarToUp.</p></body></html>";
                    msg.IsHtml = true;
                    msg.Subject = "E-mail de Confirmação - StarToUp";
                    msg.ToEmail = startup.Email;
                    gmail.SendEmailMessage(msg);

                    return RedirectToAction("../Home/Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.FotoMensagem = "Não foi possível salvar a foto";
            }
            ViewBag.StartupID = new SelectList(db.Startups, "StartupID", "Nome", startup.StartupID);
            ViewBag.AvaliacaoID = new SelectList(db.Avaliacoes, "AvaliacaoID", "Descricao", startup.AvaliacaoID);
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", startup.SegmentacaoID);
            return View(startup);
        }

        // GET: Startups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Startup startup = db.Startups.Find(id);
            if (startup == null)
            {
                return HttpNotFound();
            }
            ViewBag.AvaliacaoID = new SelectList(db.Avaliacoes, "AvaliacaoID", "Descricao", startup.AvaliacaoID);
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", startup.SegmentacaoID);
            return View(startup);
        }

        // POST: Startups/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StartupID,Nome,Email,Senha,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Sobre,DataFundacao,Logotipo,ImgStartup1,ImgStartup2,ImagemMVP1,ImagemMVP2,LinkInstagram,LinkFacebook,LinkLinkedin,TermoUso,Hash,SegmentacaoID,AvaliacaoID")] Startup startup,
            HttpPostedFileBase logoTipo, HttpPostedFileBase imgStartup1, HttpPostedFileBase imgStartup2, HttpPostedFileBase imagemMVP1, HttpPostedFileBase imagemMVP2)
        {
            ViewBag.FotoMensagem = "";
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = "";
                    string contentType = "";
                    string path = "";

                    Startup startupBD = db.Startups.Find(startup.StartupID);
                    if (logoTipo != null && logoTipo.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(logoTipo.FileName);
                        contentType = logoTipo.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\PerfilStartup\\" + fileName;
                        logoTipo.SaveAs(path);
                        startup.Logotipo = fileName;
                    }
                    else
                    {
                        if (logoTipo == null)
                        {
                            if (startupBD.Logotipo != null)
                            {
                                if (startupBD.Logotipo.Length > 0)
                                {
                                    //usa valores que ja estao no BD
                                    startup.Logotipo = startupBD.Logotipo;
                                }
                            }
                        }
                    }

                    if (imgStartup1 != null && imgStartup1.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imgStartup1.FileName);
                        contentType = imgStartup1.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\Startup\\" + fileName;
                        imgStartup1.SaveAs(path);
                        startup.ImgStartup1 = fileName;
                    }
                    else
                    {
                        if (imgStartup1 == null)
                        {
                            if (startupBD.ImgStartup1 != null)
                            {
                                if (startupBD.ImgStartup1.Length > 0)
                                {
                                    //usa valores que ja estao no BD
                                    startup.ImgStartup1 = startupBD.ImgStartup1;
                                }
                            }
                        }
                    }

                    if (imgStartup2 != null && imgStartup2.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imgStartup2.FileName);
                        contentType = imgStartup2.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\Startup\\" + fileName;
                        imgStartup2.SaveAs(path);
                        startup.ImgStartup2 = fileName;
                    }
                    else
                    {
                        if (imgStartup2 == null)
                        {
                            if (startupBD.ImgStartup2 != null)
                            {
                                if (startupBD.ImgStartup2.Length > 0)
                                {
                                    //usa valores que ja estao no BD
                                    startup.ImgStartup2 = startupBD.ImgStartup2;
                                }
                            }
                        }
                    }

                    if (imagemMVP1 != null && imagemMVP1.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemMVP1.FileName);
                        contentType = imagemMVP1.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\Startup\\" + fileName;
                        imagemMVP1.SaveAs(path);
                        startup.ImagemMVP1 = fileName;
                    }
                    else
                    {
                        if (imagemMVP1 == null)
                        {
                            if (startupBD.ImagemMVP1 != null)
                            {
                                if (startupBD.ImagemMVP1.Length > 0)
                                {
                                    //usa valores que ja estao no BD
                                    startup.ImagemMVP1 = startupBD.ImagemMVP1;
                                }
                            }
                        }
                    }

                    if (imagemMVP2 != null && imagemMVP2.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(imagemMVP2.FileName);
                        contentType = imagemMVP2.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\Startup\\" + fileName;
                        imagemMVP2.SaveAs(path);
                        startup.ImagemMVP2 = fileName;
                    }
                    else
                    {
                        if (imagemMVP2 == null)
                        {
                            if (startupBD.ImagemMVP2 != null)
                            {
                                if (startupBD.ImagemMVP2.Length > 0)
                                {
                                    //usa valores que ja estao no BD
                                    startup.ImagemMVP2 = startupBD.ImagemMVP2;
                                }
                            }
                        }
                    }

                    ((IObjectContextAdapter)db).ObjectContext.Detach(startupBD);
                    db.Entry(startup).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.FotoMensagem = "Não foi possível salvar a foto";
            }
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", startup.SegmentacaoID);
            ViewBag.AvaliacaoID = new SelectList(db.Avaliacoes, "AvaliacaoID", "Descricao", startup.AvaliacaoID);
            return View(startup);
        }

        public ActionResult SearchStartup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchStartup(FormCollection fc, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var startup = db.Startups.Include(s => s.Segmentacoes).Where(s => s.Segmentacoes.Descricao.Contains(searchString) || s.Nome.Contains(searchString) || s.Sobre.Contains(searchString) || s.Cep.Contains(searchString)).OrderByDescending(o => o.StartupID);
                return View("SearchStartup", startup.ToList());
            }
            else
            {
                return RedirectToAction("SearchStartup");
            }
        }

        // GET: Startups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Startup startup = db.Startups.Find(id);
            if (startup == null)
            {
                return HttpNotFound();
            }
            return View(startup);
        }

        // POST: Startups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Startup startup = db.Startups.Find(id);
            db.Startups.Remove(startup);
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

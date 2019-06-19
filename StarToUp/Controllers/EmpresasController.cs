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
    public class EmpresasController : Controller
    {
        private Context db = new Context();

        // GET: Empresas
        public ActionResult Index()
        {
            var empresas = db.Empresas.Include(e => e.Segmentacoes);
            return View(empresas.ToList());
        }

        public ActionResult Blog()
        {
            if (Session["Usuario"] != null)
            {
                FuncoesEmpresa.GetUsuarioEmpresa();

                var empresas = db.Empresas.Include(e => e.Segmentacoes);
                IEnumerable<Startup> startups = db.Startups.ToList().Take(3);
                ViewBag.Startups = startups;
                ViewBag.Empresas = empresas;
                return View(empresas.ToList().Take(3));
            }
            else
            {
                return RedirectToAction("Logar", "LogonEmpresa");
            }
        }

        public ActionResult Logoff()
        {
            StarToUp.Repositories.Funcoes.Deslogar();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Startups()
        {
            var startups = db.Startups.Include(s => s.Avaliacoes).Include(s => s.Segmentacoes);
            return View(startups.ToList());
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
                return RedirectToAction("Blog");
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

        // GET: Startups/Details/5
        public ActionResult DetalheStartup(int? id)
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
        public ActionResult DetalheStartup([Bind(Include = "StartupID,Nome,Email,Senha,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Sobre,DataFundacao,Logotipo,ImgStartup1,ImgStartup2,ImagemMVP1,ImagemMVP2,LinkInstagram,LinkFacebook,LinkLinkedin,TermoUso,Hash,SegmentacaoID,AvaliacaoID")] Startup startup,
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

            return RedirectToAction("Blog");
        }

        // GET: Empresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // GET: Empresas/Create
        public ActionResult Create()
        {
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao");
            return View();
        }

        // POST: Empresas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpresaID,Nome,Email,Senha,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Logotipo,Sobre,RazaoSocial,LinkInstagram,LinkFacebook,LinkLinkedin,TermoUso,Hash,SegmentacaoID")] Empresa empresa, HttpPostedFileBase logotipo)
        {
            ViewBag.FotoMensagem = "";
            try
            {
                if (ModelState.IsValid && empresa.TermoUso == true)
                {
                    string fileName = "";
                    string contentType = "";
                    string path = "";
                    if (logotipo != null && logotipo.ContentLength > 0)
                    {
                        fileName = System.IO.Path.GetFileName(logotipo.FileName);
                        contentType = logotipo.ContentType;
                        path = System.Configuration.ConfigurationManager.AppSettings["PathFiles"] + "\\Empresa\\" + fileName;
                        logotipo.SaveAs(path);
                        empresa.Logotipo = fileName;
                    }

                    db.Empresas.Add(empresa);
                    db.SaveChanges();

                    GmailEmailService gmail = new GmailEmailService();
                    EmailMessage msg = new EmailMessage();
                    msg.Body = "<!DOCTYPE HTML><html><body><p>" + empresa.Nome + ",<br/>Seja bem-vinda(o)!</p><p>Sua decolagem está prestes a iniciar!<br/>Clique no link abaixo para finalizar seu cadastro:</p><a href= http://localhost:50072/LogonEmpresa/Logar/" + "> Faça seu login aqui!</a><p>Esperamos que você decole com a gente!</p><p>Atenciosamente,<br/>StarToUp.</p></body></html>";
                    msg.IsHtml = true;
                    msg.Subject = "E-mail de Confirmação - StarToUp";
                    msg.ToEmail = empresa.Email;
                    gmail.SendEmailMessage(msg);

                    return RedirectToAction("../Home/Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.FotoMensagem = "Não foi possível salvar a foto";
            }
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", empresa.SegmentacaoID);
            ViewBag.StartupCadastroID = new SelectList(db.Startups, "StartupID", "Nome", empresa.EmpresaID);
            return View(empresa);
        }

        // GET: Empresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", empresa.SegmentacaoID);
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpresaID,Nome,Email,Senha,Cep,Rua,Bairro,Numero,Complemento,Cidade,Estado,Logotipo,Sobre,RazaoSocial,LinkInstagram,LinkFacebook,LinkLinkedin,TermoUso,Hash,SegmentacaoID")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SegmentacaoID = new SelectList(db.Segmentacoes, "SegmentacaoID", "Descricao", empresa.SegmentacaoID);
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empresa empresa = db.Empresas.Find(id);
            db.Empresas.Remove(empresa);
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

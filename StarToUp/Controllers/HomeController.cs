using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StarToUp.Models;
using StarToUp.Repositories;

namespace StarToUp.Controllers
{
    public class HomeController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            IEnumerable<Startup> startups = db.Startups.ToList().Take(3);
            IEnumerable<Empresa> empresas = db.Empresas.ToList().Take(3);
            IEnumerable<Evento> eventos = db.Eventos.ToList();
            IEnumerable<Segmentacao> segmentacoes = db.Segmentacoes.ToList();
            var query = from c in db.Segmentacoes
                        join p in db.Startups
                        on c.SegmentacaoID equals p.SegmentacaoID
                        group new { c, p } by new { c.Descricao } into g
                        select new SegmentacaoStartupGroup
                        {
                            Descricao = g.Key.Descricao,
                            //SumPrice = (decimal?)g.Sum(pt => pt.p.UnitPrice),
                            Count = g.Count()
                        };

            ViewBag.Segmentos = (IEnumerable<Models.SegmentacaoStartupGroup>)query.ToList();

            var queryy = from c in db.Segmentacoes
                         join e in db.Empresas
                         on c.SegmentacaoID equals e.SegmentacaoID
                         group new { c, e } by new { c.Descricao } into g
                         select new SegmentacaoEmpresaGroup
                         {
                             Descricao = g.Key.Descricao,
                             //SumPrice = (decimal?)g.Sum(pt => pt.p.UnitPrice),
                             Count = g.Count()
                         };

            ViewBag.SegmentoEmpresa = (IEnumerable<Models.SegmentacaoEmpresaGroup>)queryy.ToList();

            ViewBag.Startups = startups;
            ViewBag.Empresas = empresas;
            ViewBag.Eventos = eventos;
            ViewBag.Segmentacoes = segmentacoes;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "CadastroID,Nome,Email,Assunto,Mensagem")] Contato contato)
        {
            if (ModelState.IsValid)
            {
                db.Contatos.Add(contato);
                db.SaveChanges();

                GmailEmailService gmail = new GmailEmailService();
                EmailMessage msg = new EmailMessage();
                msg.Body = "<!DOCTYPE HTML><html><body><p>Mensagem de: <b>" + contato.Nome + "</b><br /><br /> " + contato.Mensagem + "</p><p>Contato do remetente para retorno: " + contato.Email + "</p></body></html>";
                msg.IsHtml = true;
                msg.Subject = contato.Assunto;
                msg.ToEmail = "startoupcontact@gmail.com";
                gmail.SendEmailMessage(msg);

                return RedirectToAction("Index");
            }

            return View();
        }

    }
}
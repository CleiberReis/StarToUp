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
            //if (Session["Usuario"] != null)
            //{
            //    Funcoes.GetUsuario();
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Logar", "Logon");
            //}
            //IEnumerable<PerfilStartup> perfilStartups = db.PerfilStartups.ToList();
            //ViewBag.PerfilStartups = perfilStartups;
           
            IEnumerable<Evento> eventos = db.Eventoes.ToList();
            ViewBag.Eventos = eventos;
            return View();
        }

        public ActionResult Login()
        {
            return View();
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
        //                return RedirectToAction("Index");
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CadastroID,Nome,Email,Assunto,Mensagem")] Contato contato)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.contato.Add(contato);
        //        db.SaveChanges();
        //        Session["ContatoID"] = Contato;

        //        GmailEmailService gmail = new GmailEmailService();
        //        EmailMessage msg = new EmailMessage();
        //        msg.Body = "<!DOCTYPE HTML><html><body><p>" + Contato.Nome + ",<br/>Seja bem-vinda(o)!</p><p>Sua decolagem está prestes a iniciar!<br/>Clique no link abaixo para finalizar seu cadastro:</p><a href= http://localhost:50072/Logon/Logar/" + "> Faça seu login aqui!</a><p>Esperamos que você decole com a gente!</p><p>Atenciosamente,<br/>StarToUp.</p></body></html>";
        //        msg.IsHtml = true;
        //        msg.Subject = "E-mail de Confirmação - StarToUp";
        //        msg.ToEmail = Contato.Email;
        //        gmail.SendEmailMessage(msg);

        //        var response = Request["g-recaptcha-response"];
        //        //chave secreta que foi gerada no site
        //        const string secret = "6Ldjv5gUAAAAAE8AgNayyITU99Lexs-BEeZU4imx";
        //        var client = new WebClient();
        //        var reply =
        //        client.DownloadString(

        //       string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
        //       secret, response));
        //        var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
        //        //Response false – devemos ver qual a mensagem de erro
        //        if (!captchaResponse.Success)
        //        {
        //            if (captchaResponse.ErrorCodes.Count <= 0) return View();
        //            var error = captchaResponse.ErrorCodes[0].ToLower();
        //            switch (error)
        //            {
        //                case ("missing-input-secret"):
        //                    ViewBag.Message = "The secret parameter is missing.";
        //                    break;
        //                case ("invalid-input-secret"):
        //                    ViewBag.Message = "The secret parameter is invalid or malformed.";
        //                    break;
        //                case ("missing-input-response"):
        //                    ViewBag.Message = "The response parameter is missing.";
        //                    break;
        //                case ("invalid-input-response"):
        //                    ViewBag.Message = "The response parameter is invalid or malformed.";
        //                    break;
        //                default:
        //                    ViewBag.Message = "Error occured. Please try again";
        //                    break;
        //            }
        //            return View();
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Valid";
        //            return RedirectToAction("../Home/Index");
        //        }

        //    }

        //    ViewBag.TipoUsuarioID = new SelectList(db.TipoUsuarios, "TipoUsuarioID", "Descricao", empresaCadastro.TipoUsuarioID);
        //    return View(Contato);
        //}

    }
}
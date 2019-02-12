
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using DAL;
using ApplicationMVC.Models;
using System.Web;
using ApplicationMVC.Areas.Prestataire.Models;

namespace ApplicationMVC.Areas.Prestataire.Controllers
{
    public class FacturationController : Controller
    {
        AssBdEntities db = new AssBdEntities();

        ViewModelAllClass model = new ViewModelAllClass();

        public ActionResult Index()
        {
            HttpCookie aCookie = Request.Cookies["userName"];
            string name = Server.HtmlEncode(aCookie.Value);
            CompteUtilisateur actif = db.CompteUtilisateur.Single(p => p.NomUtilisateur == name);
            if (actif.CodeProfil != "PREST")
            {
                return View("AccesInterdit");
            }

            HttpCookie aCookie_2 = Request.Cookies["username"];
            string username = Server.HtmlEncode(aCookie_2.Value);
            model.prest = db.Prestataire.Single(p => p.NomOuRaisonSociale == username);
            model.facture = db.Facture.ToList();
            model.affilie = db.Affilie.ToList();

            return View(model);
        }

        public ActionResult ValiderClient(string CodeSecret)
        {
            HttpCookie aCookie = Request.Cookies["userName"];
            string name = Server.HtmlEncode(aCookie.Value);
            CompteUtilisateur actif = db.CompteUtilisateur.Single(p => p.NomUtilisateur == name);
            if (actif.CodeProfil != "PREST")
            {
                return View("AccesInterdit");
            }
            Affilie affilie = null;
            if (!String.IsNullOrEmpty(CodeSecret))
            {
                affilie = db.Affilie.Single(p => p.CodeSecret == CodeSecret);
                Response.Cookies["name"].Value = affilie.Nom;
                Response.Cookies["name"].Expires = DateTime.Now.AddDays(7);
            }
            if (affilie == null)
            {
                return View();
            }
            return View(affilie);
        }

        [HttpPost]
        public ActionResult Facturer(FactureNew fact, string total)
        {
            HttpCookie aCookie = Request.Cookies["userName"];
            string names = Server.HtmlEncode(aCookie.Value);
            CompteUtilisateur actif = db.CompteUtilisateur.Single(p => p.NomUtilisateur == names);
            if (actif.CodeProfil != "PREST")
            {
                return View("AccesInterdit");
            }
            double To = double.Parse(total);
            
            int idMax = db.Facture.Count();

            HttpCookie aCookie_1 = Request.Cookies["name"];
            string name = Server.HtmlEncode(aCookie_1.Value);
            HttpCookie aCookie_2 = Request.Cookies["username"];
            string username = Server.HtmlEncode(aCookie_2.Value);
            var rqt1 = db.CompteUtilisateur.Single(p => p.NomUtilisateur == username);
            var rqt2 = db.Prestataire.Single(p => p.NomOuRaisonSociale == username);

            var rqt3 = db.Affilie.FirstOrDefault();

            Facture facture = new Facture
            {
                IdAdherent = rqt3.IdAdherent,
                IdAffilie = rqt3.IdAffilie,
                IdPrestataire = rqt2.IdPrestataire,
                IdPlafond = rqt2.IdPlafond,
                IdPolice = rqt3.Adherent.IdPolice,
                IdFacture = idMax + 1,
                DateFacture = DateTime.Now.Date,
                Montant = To,
                RestePlafond = rqt3.MontantPlafond - To,
            };
            db.Facture.Add(facture);
            db.SaveChanges();

            var aff = db.Affilie.ToList().Where(p => p.IdAffilie == rqt3.IdAffilie);
            var t = aff.Last();
            t.MontantRestantPlafon = rqt3.MontantPlafond - To;
            db.Affilie.AddOrUpdate(t);
            db.SaveChanges();

            return View();
        }
    }
}
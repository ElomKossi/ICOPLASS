using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using ApplicationMVC.Areas.Listes.Models;

namespace ApplicationMVC.Areas.Listes.Controllers
{
    public class ListesController : Controller
    {
        private AssBdEntities db = new AssBdEntities();

        public ListViewModel model = new ListViewModel();
        
        public ActionResult Index()
        {
            HttpCookie aCookie = Request.Cookies["userName"];
            string name = Server.HtmlEncode(aCookie.Value);
            CompteUtilisateur actif = db.CompteUtilisateur.Single(p => p.NomUtilisateur == name);
            if (actif.CodeProfil != "PERSO")
            {
                return View("AccesInterdit");
            }
            model.prest = db.Prestataire.ToList();
            model.adher = db.Adherent.ToList();
            model.affil = db.Affilie.ToList();
            model.polic = db.Police.ToList();
            model.factu = db.Facture.ToList();
            model.PP = db.Ligne_PolicePlafond.ToList();

            return View(model);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using ApplicationMVC.Models;
using System.Data.Entity.Migrations;

namespace ApplicationMVC.Controllers
{
    public class UserController : Controller
    {
        public AssBdEntities Bd = new AssBdEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Parametre(CompteUtilisateur J)
        {
            var tmp = Bd.CompteUtilisateur.Single(p => p.IdUtilisateur == J.IdUtilisateur);
            tmp.IdUtilisateur = J.IdUtilisateur;
            tmp.Login = J.Login;
            Bd.CompteUtilisateur.AddOrUpdate(tmp);
            Bd.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Motdepasse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Motdepasse(string motdepasse, string motdepasse1, string confirm)
        {
            HttpCookie aCookie = Request.Cookies["userName"];
            CompteUtilisateur courant = Bd.CompteUtilisateur.Single(p => p.NomUtilisateur == Server.HtmlEncode(aCookie.Value));

            if (courant.MotDePasse == motdepasse)
            {
                if (motdepasse1 == confirm)
                {
                    courant.MotDePasse = motdepasse1;
                }
            }
            else
            {
                
            }
            return View();
        }

    }
}
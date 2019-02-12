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

namespace ApplicationMVC.Controllers
{
    public class LoginController : Controller
    {
        public AssBdEntities db = new AssBdEntities();

        public int Tentatives = 0;

        UserViewModel model = new UserViewModel();

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return Redirect("Acceuil/Index");
            List<Connexion> list = new List<Connexion>();
            foreach (var connexion in db.Connexion)
                list.Add(connexion);
            model.Connexions = list;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Connexion(CompteUtilisateur logUtilisateur)
        {
            Tentatives++;
            if (Tentatives >= 4)
            {
                return RedirectToAction("Block");
            }
            try
            {
                db.CompteUtilisateur.Single(p => p.Login == logUtilisateur.Login);
                db.SaveChanges();
            }
            catch (Exception x)
            {
                ViewBag.erreur = x.Message;

            }
            List<Profil> profils = db.Profil.ToList();
            try
            {
                model.User = db.CompteUtilisateur.FirstOrDefault(u => u.Login == logUtilisateur.Login && u.MotDePasse == logUtilisateur.MotDePasse);
            }
            catch (Exception)
            {
                ViewBag.erreur = "Identifiant ou Mot de passe incorrect";
                return View("Index");
            }
            if (model.User == null)
            {
                ViewBag.erreur = "Identifiant ou Mot de passe incorrect";
                return View("Index");
            }
            else
            {
                if (model.User.Actif == true)
                 {
                    Response.Cookies [ "username"].Value = model.User.NomUtilisateur;
                    Response.Cookies [ "username"].Expires =  DateTime.Now.AddDays(7);

                    HttpCookie aCookie = new HttpCookie("lastVisit");
                    aCookie.Value = DateTime.Now.ToString();
                    aCookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(aCookie);

                    int IdMax = db.Connexion.Count();
                    int result = IdMax + 1;

                    Connexion connect = new Connexion
                    {
                        IdConnexion = result,
                        IdUtilisateur = model.User.IdUtilisateur,
                        Debut = DateTime.Now,
                    };
                    Debug.Assert(Request.UserHostAddress != null, "Request.UserHostAddress != null");
                    connect.adresseIp = Request.ServerVariables["REMOTE_ADDR"];
                    db.Connexion.Add(connect);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.erreur = " Compte désactivé veuillez contacter l'Administration";
                    return View("Index");
                }
            }
        }

        //[Authorize]
        public ActionResult Deconnexion()
        {
            HttpCookie aCookie = Request.Cookies["userName"];
            var c = db.Connexion.ToList().Where(p => p.CompteUtilisateur.NomUtilisateur == Server.HtmlEncode(aCookie.Value) && p.Fin == null);
            try
            {
                var t = c.Last();
                t.Fin = DateTime.Now;
                db.Connexion.AddOrUpdate(t);
                db.SaveChanges();
                FormsAuthentication.SignOut();     
            }
            catch (Exception)
            {

            }
            return Redirect("/");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace ApplicationMVC.Areas.Administration.Controllers
{
    public class IndexController : Controller
    {
        private AssBdEntities db = new AssBdEntities();
        
        // GET: Administration/Index
        public ActionResult Index()
        {
            HttpCookie aCookie = Request.Cookies["userName"];
            string name = Server.HtmlEncode(aCookie.Value);
            CompteUtilisateur actif = db.CompteUtilisateur.Single(p => p.NomUtilisateur == name);
            if (actif.CodeProfil != "ADMIN")
            {
                return View("AccesInterdit");
            }
            return View();
        }
    }
}
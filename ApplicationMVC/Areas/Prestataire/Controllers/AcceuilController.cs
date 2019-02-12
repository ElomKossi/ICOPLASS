using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApplicationMVC.Areas.Prestataire.Controllers
{
    public class AcceuilController : Controller
    {
        // GET: Prestataire/Acceuil
        public ActionResult Index()
        {
            return View();
        }
    }
}
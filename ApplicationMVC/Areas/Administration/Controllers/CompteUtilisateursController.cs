using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using ApplicationMVC.Areas.Administration.Models;

namespace ApplicationMVC.Areas.Administration.Controllers
{
    public class CompteUtilisateursController : Controller
    {
        private AssBdEntities db = new AssBdEntities();
        public AllViewModel Model = new AllViewModel();

        // GET: Administration/CompteUtilisateurs
        public ActionResult Index()
        {
            var compteUtilisateur = db.CompteUtilisateur.Include(c => c.Profil);
            Model.user = db.CompteUtilisateur.ToList();
            Model.profil = db.Profil.ToList();
            return View(Model);
        }

        // GET: Administration/CompteUtilisateurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompteUtilisateur compteUtilisateur = db.CompteUtilisateur.Find(id);
            if (compteUtilisateur == null)
            {
                return HttpNotFound();
            }
            return View(compteUtilisateur);
        }

        // GET: Administration/CompteUtilisateurs/Create
        public ActionResult Create()
        {
            ViewBag.CodeProfil = new SelectList(db.Profil, "CodeProfil", "NomProfil");
            //Model.profil = db.Profil.ToList();

            return View();
        }

        // POST: Administration/CompteUtilisateurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUtilisateur,NomUtilisateur,Login,MotDePasse,DateCreationCompte,Actif,CodeProfil")] CompteUtilisateur compteUtilisateur)
        {
            Model.profil = db.Profil.ToList();
            if (ModelState.IsValid)
            {
                db.CompteUtilisateur.Add(compteUtilisateur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodeProfil = new SelectList(db.Profil, "CodeProfil", "NomProfil", compteUtilisateur.CodeProfil);
            return View(compteUtilisateur);
        }

        // GET: Administration/CompteUtilisateurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompteUtilisateur compteUtilisateur = db.CompteUtilisateur.Find(id);
            if (compteUtilisateur == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodeProfil = new SelectList(db.Profil, "CodeProfil", "NomProfil", compteUtilisateur.CodeProfil);
            return View(compteUtilisateur);
        }

        // POST: Administration/CompteUtilisateurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUtilisateur,NomUtilisateur,Login,MotDePasse,DateCreationCompte,Actif,CodeProfil")] CompteUtilisateur compteUtilisateur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compteUtilisateur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodeProfil = new SelectList(db.Profil, "CodeProfil", "NomProfil", compteUtilisateur.CodeProfil);
            return View(compteUtilisateur);
        }

        // GET: Administration/CompteUtilisateurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompteUtilisateur compteUtilisateur = db.CompteUtilisateur.Find(id);
            if (compteUtilisateur == null)
            {
                return HttpNotFound();
            }
            return View(compteUtilisateur);
        }

        // POST: Administration/CompteUtilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompteUtilisateur compteUtilisateur = db.CompteUtilisateur.Find(id);
            db.CompteUtilisateur.Remove(compteUtilisateur);
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

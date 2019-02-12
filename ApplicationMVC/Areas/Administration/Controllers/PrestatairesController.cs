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
    public class PrestatairesController : Controller
    {
        private AssBdEntities db = new AssBdEntities();
        public AllViewModel Model = new AllViewModel();

        // GET: Administration/Prestataires
        public ActionResult Index()
        {
            Model.plafond = db.Plafond.ToList();
            Model.prestataire = db.Prestataire.ToList();
            var prestataire = db.Prestataire.Include(p => p.Plafond);
            return View(Model);
        }

        // GET: Administration/Prestataires/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAL.Prestataire prestataire = db.Prestataire.Find(id);
            if (prestataire == null)
            {
                return HttpNotFound();
            }
            return View(prestataire);
        }

        // GET: Administration/Prestataires/Create
        public ActionResult Create()
        {
            ViewBag.IdPlafond = new SelectList(db.Plafond, "IdPlafond", "LibellePlafond");
            Model.plafond = db.Plafond.ToList();

            return View(Model);
        }

        // POST: Administration/Prestataires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPrestataire,NomOuRaisonSociale,Email,Telephone,Adresse,IdPlafond")] DAL.Prestataire prestataire)
        {
            Model.plafond = db.Plafond.ToList();
            if (ModelState.IsValid)
            {
                prestataire.IdPrestataire = db.Prestataire.Max(p => p.IdPrestataire);
                db.Prestataire.Add(prestataire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPlafond = new SelectList(db.Plafond, "IdPlafond", "LibellePlafond", prestataire.IdPlafond);
            return View(prestataire);
        }

        // GET: Administration/Prestataires/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAL.Prestataire prestataire = db.Prestataire.Find(id);
            if (prestataire == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPlafond = new SelectList(db.Plafond, "IdPlafond", "LibellePlafond", prestataire.IdPlafond);
            return View(prestataire);
        }

         //POST: Administration/Prestataires/Edit/5
         //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPrestataire,NomOuRaisonSociale,Email,Telephone,Adresse,IdPlafond")] DAL.Prestataire prestataire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestataire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPlafond = new SelectList(db.Plafond, "IdPlafond", "LibellePlafond", prestataire.IdPlafond);
            return View(prestataire);
        }

        // GET: Administration/Prestataires/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAL.Prestataire prestataire = db.Prestataire.Find(id);
            if (prestataire == null)
            {
                return HttpNotFound();
            }
            return View(prestataire);
        }

        // POST: Administration/Prestataires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DAL.Prestataire prestataire = db.Prestataire.Find(id);
            db.Prestataire.Remove(prestataire);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ComicBookStore.Models;

namespace ComicBookStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ComicModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ComicModels
        public ActionResult ManageComics()
        {
            return View(db.Comics.ToList());
        }

        // GET: ComicModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComicModel comicModel = db.Comics.Find(id);
            if (comicModel == null)
            {
                return HttpNotFound();
            }
            return View(comicModel);
        }

        // GET: ComicModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ComicModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Year,Writer,Desc,Img,Type,Price,inCarousel,isDiscounted,DiscountPrice")] ComicModel comicModel)
        {
            if (ModelState.IsValid)
            {
                db.Comics.Add(comicModel);
                db.SaveChanges();
                return RedirectToAction("ManageComics");
            }

            return View(comicModel);
        }

        // GET: ComicModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComicModel comicModel = db.Comics.Find(id);
            if (comicModel == null)
            {
                return HttpNotFound();
            }
            return View(comicModel);
        }

        // POST: ComicModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Year,Writer,Desc,Img,Type,Price,inCarousel,isDiscounted,DiscountPrice")] ComicModel comicModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comicModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageComics");
            }
            return View(comicModel);
        }

        // GET: ComicModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComicModel comicModel = db.Comics.Find(id);
            if (comicModel == null)
            {
                return HttpNotFound();
            }
            return View(comicModel);
        }

        // POST: ComicModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ComicModel comicModel = db.Comics.Find(id);
            db.Comics.Remove(comicModel);
            db.SaveChanges();
            return RedirectToAction("ManageComics");
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

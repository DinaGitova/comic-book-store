using ComicBookStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComicBookStore.Controllers
{
    public class HomeController : Controller
    {
        protected ApplicationDbContext db;
        private List<ComicModel> comics = new List<ComicModel>();
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public HomeController()
        {
            db = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
        }

        public ActionResult Index()
        {

            comics = db.Comics.ToList();
            return View(comics);
        }

        public ActionResult PrintComics()
        {
            comics = db.Comics.Where(c => c.Type.Equals("Print")).ToList();
            return View(comics);
        }

        public ActionResult DigitalComics()
        {

            comics = db.Comics.Where(c => c.Type.Equals("Digital")).ToList();
            return View("PrintComics", comics);
        }

        public ActionResult About()
        {
            ViewBag.Message = "About";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact information";

            return View();
        }

        public ActionResult Search(string search)
        {
            search = search.ToLower();
            comics = db.Comics.Where(c => c.Title.ToLower().Contains(search) || c.Writer.ToLower().Contains(search)).ToList();
            return View(comics);
        }
        [Authorize]
        public ActionResult AddToList(int id, string value)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var model = db.UserComic.SingleOrDefault(u => u.UserId.Equals(user.Id));
            ComicModel comic = db.Comics.SingleOrDefault(c => c.Id == id);
            if (value == "cart")
            {
                if (model != null)
                {
                    if (!model.CartComics.Contains(comic))
                    {
                        model.CartComics.Add(comic);
                        db.SaveChanges();
                    }
                }
                else
                {
                    var cart = new LinkedList<ComicModel>();
                    cart.AddFirst(comic);
                    db.UserComic.Add(new UserComicModel(user.Id, new LinkedList<ComicModel>(), cart));
                    db.SaveChanges();
                }
            }
            else if (value == "fav")
            {
                if (model != null)
                {
                    if (!model.FavouriteComics.Contains(comic))
                    {
                        model.FavouriteComics.Add(comic);
                        db.SaveChanges();
                    }
                }
                else
                {
                    var fav = new LinkedList<ComicModel>();
                    fav.AddFirst(comic);
                    db.UserComic.Add(new UserComicModel(user.Id, fav, new LinkedList<ComicModel>()));
                    db.SaveChanges();
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        [Authorize]
        public ActionResult ViewList(string value)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var model = db.UserComic.SingleOrDefault(u => u.UserId.Equals(user.Id));

            if (model != null)
            {
                if (value == "cart") { comics = model.CartComics.ToList<ComicModel>(); ViewBag.Type = "Cart: "; }
                if (value == "fav") { comics = model.FavouriteComics.ToList<ComicModel>(); ViewBag.Type = "Favourites: "; }
            }

            if (value == "fav") return View("Favourites", comics);
            return View(comics);
        }

        public ActionResult RemoveComic(int id, string value)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var model = db.UserComic.SingleOrDefault(u => u.UserId.Equals(user.Id));
            var comic = db.Comics.SingleOrDefault(c => c.Id == id);

            if (model != null)
            {
                if (value == "cart")
                {
                    model.CartComics.Remove(comic);
                    db.SaveChanges();
                    comics = model.CartComics.ToList<ComicModel>();
                    return View("ViewList", comics);
                }
                if (value == "fav")
                {
                    model.FavouriteComics.Remove(comic);
                    db.SaveChanges();
                    comics = model.FavouriteComics.ToList<ComicModel>();
                    return View("Favourites", comics);
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult ComicDetails(int id)
        {
            ComicModel comic = db.Comics.SingleOrDefault(c => c.Id == id);
            return View(comic);
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}
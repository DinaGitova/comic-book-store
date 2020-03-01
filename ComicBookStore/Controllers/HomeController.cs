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
            ViewBag.Message = "Your application description page.";

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
            if (value == "cart")
            {
                if (model != null)
                {
                    if (!checkIfDuplicate(model.Cart, id.ToString()))
                    {
                        model.Cart += "," + id;
                        db.SaveChanges();
                    }
                }
                else
                {
                    string ids = id.ToString() + ",";
                    db.UserComic.Add(new UserComicModel(user.Id, "", ids));
                    db.SaveChanges();
                }
            }
            else if(value == "fav")
            {
                if (model != null)
                {
                    if (!checkIfDuplicate(model.FavouriteComics, id.ToString())) {
                        model.FavouriteComics += "," + id;
                        db.SaveChanges();
                    }
                }
                else
                {
                    string ids = id.ToString() + ",";
                    db.UserComic.Add(new UserComicModel(user.Id, ids, ""));
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
            string[] ids = null;
            if (model != null)
            {
                if (value == "cart") { ids = model.Cart.Split(','); ViewBag.Type = "Cart: "; }
                if (value == "fav") { ids = model.FavouriteComics.Split(','); ViewBag.Type = "Favourites: "; }
            }
            comics = getComics(ids);
            if (value == "fav") return View("Favourites", comics);
            return View(comics);
        }

        public ActionResult RemoveComic(int id, string value)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var model = db.UserComic.SingleOrDefault(u => u.UserId.Equals(user.Id));
            string ids = null; string newList = null;
            if (model != null)
            {
                if (value == "cart")
                {
                    ids = model.Cart;
                    newList = remove(ids, id.ToString());
                    model.Cart = newList;
                    db.SaveChanges();
                    comics = getComics(newList.Split(','));
                    return View("ViewList", comics);
                }
                if (value == "fav")
                {
                    ids = model.FavouriteComics;
                    newList = remove(ids, id.ToString());
                    model.FavouriteComics = newList;
                    db.SaveChanges();
                    comics = getComics(newList.Split(','));
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

        public bool checkIfDuplicate(string ids, string id)
        {
            string[] comicsId = ids.Split(',');
            if (comicsId != null)
            {
                foreach (string i in comicsId)
                {
                    if (i == id) { return true; }
                }
            }
            return false;
        }
        public string remove(string ids, string id)
        {
            List<string> comicsId = ids.Split(',').ToList<string>();
            bool removed = false;
            if (comicsId != null)
                {
                    for (int i = 0; i < comicsId.Count(); i++)
                    {
                        if (comicsId[i] == id)
                        {
                            comicsId.RemoveAt(i);
                             removed = true;
                        }
                    }
                }
            if(removed)
            {
                string newList = ",";
                for (int i = 0; i < comicsId.Count(); i++)
                {
                    if (!comicsId[i].Equals(','))
                    {
                        newList += comicsId[i] + ",";
                    }

                }
                return newList;
            }
            return ids;

        }

        public List<ComicModel> getComics(string[] ids)
        {
            if (ids != null)
            {
                foreach (string id in ids)
                {
                    if (id != "")
                    {
                        int i = Int32.Parse(id);
                        var comic = db.Comics.SingleOrDefault(c => c.Id.Equals(i));
                       if(comic != null) comics.Add(comic);
                    }
                }
                return comics;
            }
            comics.Add(new ComicModel());
            return comics;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}
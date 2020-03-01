using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComicBookStore.Models
{
    public class UserComicModel
    {
       

        [Key]
        public string UserId { get; set; }
        public string FavouriteComics { get; set; }
        public string Cart { get; set; }

        public UserComicModel() { }

        public UserComicModel(string id, string fav, string cart)
        {
            UserId = id;
            FavouriteComics = fav;
            Cart = cart;
        }
    }
}
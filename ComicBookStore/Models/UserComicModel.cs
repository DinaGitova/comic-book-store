using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ComicBookStore.Models
{
    public class UserComicModel
    {
       

        [Key]
        public string UserId { get; set; }


        [InverseProperty("Favourite")]
        public virtual ICollection<ComicModel> FavouriteComics { get; set; }
        [InverseProperty("Cart")]
        public virtual ICollection<ComicModel> CartComics { get; set; }

        public UserComicModel() { }

        public UserComicModel(string userId, ICollection<ComicModel> favouriteComics, ICollection<ComicModel> cart)
        {
            UserId = userId;
            FavouriteComics = favouriteComics;
            CartComics = cart;
        }
    }
}
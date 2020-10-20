using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComicBookStore.Models
{
    public class ComicModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Writer { get; set; }
        public string Desc { get; set; }
        public string Img { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public bool inCarousel { get; set; }
        public bool isDiscounted { get; set; }
        public int DiscountPrice { get; set; }

        public virtual ICollection<UserComicModel> Cart { get; set; }
        public virtual ICollection<UserComicModel> Favourite { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Entities.Models;

namespace Entities.ExtendedModels
{
    public class PostBlogExtended
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Plot { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int TripId { get; set; }
        public TripExtended Trip { get; set; }

        public IEnumerable<TagPostBlog> TagPostBlogs { get; set; }
        public IEnumerable<Purchase> Purchases { get; set; }
        //public ICollection<Rating> Ratings { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<CountryPostBlog> CountryPostBlogs { get; set; }

        public PostBlogExtended()
        {

        }

        public PostBlogExtended(PostBlog postBlog)
        {
            Id = postBlog.Id;
            Name = postBlog.Name;
            Plot = postBlog.Plot;
            DateOfCreation = postBlog.DateOfCreation;
            TripId = postBlog.TripId;
            Trip = postBlog.Trip;
        }
    }
}

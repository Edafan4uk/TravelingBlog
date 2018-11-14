using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Entities.Models;

namespace Entities.ExtendedModels
{
    public class TripExtended
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public int UserId { get; set; }
        public UserExtended User { get; set; }

        public IEnumerable<PostBlog> PostBlogs { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<TagTrip> TagTrips { get; set; }
        public IEnumerable<CountryTrip> CountryTrips { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }

        public TripExtended()
        {

        }

        public TripExtended(Trip trip)
        {
            Id = trip.Id;
            Name = trip.Name;
            IsDone = trip.IsDone;
            UserId = trip.UserId;
            User = trip.User;
        }
    }
}

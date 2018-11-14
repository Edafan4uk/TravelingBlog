using System.ComponentModel.DataAnnotations.Schema;
using Entities.ExtendedModels;

namespace Entities.Models
{
    [Table("Rating")]
    public class Rating
    {
        public int UserId { get; set; }
        public int TripId { get; set; }
        public bool? RatingPostBlog { get; set; }
        public UserExtended User { get; set; }
        public TripExtended Trip { get; set; }
    }
}
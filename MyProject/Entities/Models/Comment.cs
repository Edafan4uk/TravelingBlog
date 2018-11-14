using System.ComponentModel.DataAnnotations.Schema;
using Entities.ExtendedModels;

namespace Entities.Models
{
    [Table("Comment")]
    public class Comment
    {
        public int UserId { get; set; }
        public int TripId { get; set; }
        public UserExtended User { get; set; }
        public TripExtended Trip { get; set; }
        public string Content { get; set; }

    }
}

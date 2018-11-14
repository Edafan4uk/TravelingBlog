using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Entities.ExtendedModels;

namespace Entities.Models
{
    [Table("TagTrip")]
    public class TagTrip
    {
        public int TagId { get; set; }
        public int TripId { get; set; }
        public TagExtended Tag { get; set; }
        public TripExtended Post { get; set; }
    }
}

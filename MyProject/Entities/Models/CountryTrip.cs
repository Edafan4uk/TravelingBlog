using System.ComponentModel.DataAnnotations.Schema;
using Entities.ExtendedModels;

namespace Entities.Models
{
    [Table("CountryTrip")]
    public class CountryTrip
    {
        public int CountryId { get; set; }
        public int TripId { get; set; }
        public CountryExtended Country { get; set; }
        public TripExtended Trip { get; set; }
    }
}

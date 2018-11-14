using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{ 
    [Table("Country")]
    public class Country 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobCode { get; set; }
    }
}
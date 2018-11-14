using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Currency")]
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

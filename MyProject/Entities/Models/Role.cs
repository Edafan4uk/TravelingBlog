using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Role")]
    public class Role 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
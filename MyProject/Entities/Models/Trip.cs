using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Entities.ExtendedModels;

namespace Entities.Models
{
    [Table("Trip")]
    public class Trip 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public int UserId { get; set; }
        public UserExtended User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Entities.ExtendedModels;

namespace Entities.Models
{
    [Table("PostBlog")]
    public class PostBlog 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Plot { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int TripId { get; set; }
        public TripExtended Trip { get; set; }
    }
}

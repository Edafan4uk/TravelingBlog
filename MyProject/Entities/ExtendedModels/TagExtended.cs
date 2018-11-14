using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Entities.Models;

namespace Entities.ExtendedModels
{
    public class TagExtended
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<TagTrip> TagTrips { get; set; }
        public IEnumerable<TagPostBlog> TagPostBlogs { get; set; }

        public TagExtended()
        {

        }

        public TagExtended(Tag tag)
        {
            Id = tag.Id;
            Name = tag.Name;
        }
    }
}

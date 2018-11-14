using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;

namespace Entities.ExtendedModels
{ 
    public class CountryExtended
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobCode { get; set; }

        public IEnumerable<User> Users { get; set; }
        public IEnumerable<CountryTrip> CountryTrips { get; set; }
        public IEnumerable<CountryPostBlog> CountryPostBlogs { get; set; }

        public CountryExtended()
        {

        }

        public CountryExtended(Country country)
        {
            Id = country.Id;
            Name = country.Name;
            MobCode = country.MobCode;
        }
    }
}
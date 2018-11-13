using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Entities.ExtendedModels
{
    public class UserExtended
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [RegularExpression(@"0[0-9]{9}")]
        public string Phone { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? CountryId { get; set; }

        public CountryExtended Country { get; set; }

        public int RoleId { get; set; }

        public RoleExtended Role { get; set; }

        public UserImage UserImage { get; set; }

        public IEnumerable<Subscription> RelationWithUserIdNavigation { get; set; }
        public IEnumerable<Subscription> RelationWithSubscriberIdNavigation { get; set; }
        public IEnumerable<Trip> Trips { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }
        public UserExtended(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Phone = user.Phone;
            DateOfBirth = user.DateOfBirth;
            CountryId = user.CountryId;
            Country = user.Country;
            RoleId = user.RoleId;
            Role = user.Role;
            UserImage = user.UserImage;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Text;
using Entities.ExtendedModels;

namespace Entities.Models
{
    [Table("User")]
    public class User
    {
        public User()
        {

        }

        public User(ClaimsPrincipal principal)
        {
            this.principal = principal;
            this.FirstName = principal.FindFirst(ClaimTypes.GivenName).Value;
            this.Email = principal.FindFirst(ClaimTypes.Email).Value;
            this.LastName = principal.FindFirst(ClaimTypes.Surname).Value;
            this.RoleId = 1;
        }

        public int Id { get; set; }
        private ClaimsPrincipal principal;
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
    }
}

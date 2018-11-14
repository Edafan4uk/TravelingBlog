using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Entities.Models;

namespace Entities.ExtendedModels
{
    public class RoleExtended
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<User> Users { get; set; }

        public RoleExtended()
        {

        }

        public RoleExtended(Role role)
        {
            Id = role.Id;
            Name = role.Name;
        }
    }
}

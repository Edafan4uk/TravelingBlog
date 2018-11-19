using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TravelingBlog.DataAcceesLayer.Models
{
    public class ApplicationRole : IdentityRole
    {
        private static int idIncrementable = 0;
        public ApplicationRole(string name) : base(name)
        {
            Id = idIncrementable++.ToString();
        }
    }
}

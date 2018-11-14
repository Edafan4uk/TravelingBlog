using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Entities.Models;
using System.Threading.Tasks;

namespace TravelBlog
{
    public static class DBInitialize
    {
        public static void Initialize(RepositoryContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role
                    {
                        Name = "user"
                    },
                    new Role
                    {
                        Name = "admin"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}

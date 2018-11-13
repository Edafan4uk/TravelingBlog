using System;
using System.Linq;
using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Entities;
using Entities.ExtendedModels;
using System.Collections.Generic;

namespace Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public Role GetRoleById(int roleId)
        {
            return SingleOrDefault(c => c.Id.Equals(roleId));
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return FindAll()
                .OrderBy(r => r.Name);
        }

        public RoleExtended GetRoleWithDetails(int id)
        {
            return new RoleExtended(GetRoleById(id))
            {
                Users = RepositoryContext.Users
                .Where(u => u.RoleId == id),
            };
        }
    }
}

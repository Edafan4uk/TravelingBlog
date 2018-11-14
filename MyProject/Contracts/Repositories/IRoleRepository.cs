using Entities.Models;
using System.Collections.Generic;
using Entities.ExtendedModels;

namespace Contracts.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        IEnumerable<Role> GetAllRoles();
        Role GetRoleById(int roleId);
        RoleExtended GetRoleWithDetails(int roleId);
    }
}

using Entities.Models;
using Entities.ExtendedModels;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int userId);
        UserExtended GetUserWithDetails(int userId);
    }
}

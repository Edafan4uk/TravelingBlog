using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Entities;
using Entities.ExtendedModels;

namespace Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public User GetUserById(int userId)
        {
            return SingleOrDefault(c => c.Id.Equals(userId));
        }

        public IEnumerable<User> GetAllUsers()
        {
            return FindAll()
                .OrderBy(c => c.FirstName);
        }

        public UserExtended GetUserWithDetails(int id)
        {
            return new UserExtended(GetUserById(id))
            {
                RelationWithUserIdNavigation = RepositoryContext.Subscriptions
                .Where(u => u.UserId == id),

                RelationWithSubscriberIdNavigation = RepositoryContext.Subscriptions
                .Where(u => u.SubcriberId == id),

                Trips = RepositoryContext.Trips
                .Where(u => u.UserId == id),

                Comments = RepositoryContext.Comments
                .Where(u => u.UserId == id),

                Ratings = RepositoryContext.Ratings
                .Where(u => u.UserId == id),
            };
        }
    }
}

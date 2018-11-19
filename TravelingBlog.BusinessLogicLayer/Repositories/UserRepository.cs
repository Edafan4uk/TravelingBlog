using System;
using System.Collections.Generic;
using System.Linq;
using TravelingBlog.BusinessLogicLayer.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using TravelingBlog.DataAcceesLayer.Models.Entities;
using TravelingBlog.DataAcceesLayer.Data;

namespace TravelingBlog.BusinessLogicLayer.Repositories
{
    public class UserRepository : Repository<UserInfo>,IUserRepository
    {
        public UserRepository(ApplicationDbContext ApplicationDbContext) : base(ApplicationDbContext)
        {

        }

        public UserInfo GetUserById(int userId)
        {
            return SingleOrDefault(c => c.Id.Equals(userId));
        }

        public IEnumerable<UserInfo> GetAllUsers()
        {
            return FindAll()
                .OrderBy(c => c.FirstName);
        }
        public async System.Threading.Tasks.Task<UserInfo> GetUserByIdentityId(string id)
        {
            var customer = await ApplicationDbContext.UserInfoes.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == id);
            return customer;
        }
    }
}

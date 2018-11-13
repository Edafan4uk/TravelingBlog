using System;
using Contracts.Repositories;

namespace Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITripRepository Trips { get; }
        IPostBlogRepository PostBlogs { get; }
        ICountryRepository Countries { get; }
        ITagRepository Tags { get; }
        IRoleRepository Roles { get; }
        int Complete();
    }
}

using System;
using Contracts.Repositories;
using Contracts;
using Entities;

namespace Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RepositoryContext repositoryContext;

        private IUserRepository users;
        private ITripRepository trips;
        private IPostBlogRepository postBlogs;
        private ICountryRepository countries;
        private ITagRepository tags;
        private IRoleRepository roles;

        public UnitOfWork(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }


        public IUserRepository Users
        {
            get
            {
                if (this.users == null)
                {
                    this.users = new UserRepository(repositoryContext);
                }
                return users;
            }
        }

        public ITripRepository Trips
        {
            get
            {
                if (this.trips == null)
                {
                    this.trips = new TripRepository(repositoryContext);
                }
                return trips;
            }
        }

        public IPostBlogRepository PostBlogs
        {
            get
            {
                if (this.postBlogs == null)
                {
                    this.postBlogs = new PostBlogRepository(repositoryContext);
                }
                return postBlogs;
            }
        }

        public ICountryRepository Countries
        {
            get
            {
                if (this.countries == null)
                {
                    this.countries = new CountryRepository(repositoryContext);
                }
                return countries;
            }
        }

        public ITagRepository Tags
        {
            get
            {
                if (this.tags == null)
                {
                    this.tags = new TagRepository(repositoryContext);
                }
                return tags;
            }
        }

        public IRoleRepository Roles
        {
            get
            {
                if (this.roles == null)
                {
                    this.roles = new RoleRepository(repositoryContext);
                }
                return roles;
            }
        }

        public int Complete()
        {
            return repositoryContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    repositoryContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

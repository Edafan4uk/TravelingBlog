using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Contracts.Repositories;
using Entities;
using Entities.ExtendedModels;
using System.Collections.Generic;

namespace Repositories
{
    public class PostBlogRepository : Repository<PostBlog>, IPostBlogRepository
    {
        public PostBlogRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public PostBlog GetPostBlogById(int postBlogId)
        {
            return SingleOrDefault(c => c.Id.Equals(postBlogId));
        }

        public IEnumerable<PostBlog> GetAllPostBlogs()
        {
            return FindAll()
                .OrderBy(pb => pb.Name);
        }

        public PostBlogExtended GetPostBlogWithDetails(int id)
        {
            return new PostBlogExtended(GetPostBlogById(id))
            {
                TagPostBlogs = RepositoryContext.TagPostBlogs
                .Where(tpb => tpb.PostBlogId == id),

                Purchases = RepositoryContext.Purchases
                .Where(p => p.PostBlogId == id),

                CountryPostBlogs = RepositoryContext.CountryPostBlogs
                .Where(cpb => cpb.PostBlogId == id),

                Images = RepositoryContext.Images
                .Where(i => i.PostBlogId == id),
            };
        }
    }
}

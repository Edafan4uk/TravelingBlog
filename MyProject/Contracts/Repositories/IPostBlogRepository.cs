using Entities.Models;
using System.Collections.Generic;
using Entities.ExtendedModels;

namespace Contracts.Repositories
{
    public interface IPostBlogRepository: IRepository<PostBlog>
    {
        IEnumerable<PostBlog> GetAllPostBlogs();
        PostBlog GetPostBlogById(int postBlogId);
        PostBlogExtended GetPostBlogWithDetails(int postBlogId);
    }
}

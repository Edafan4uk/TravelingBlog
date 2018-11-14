using System;
using System.Linq;
using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Entities.ExtendedModels;
using Entities;
using System.Collections.Generic;

namespace Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public Tag GetTagById(int tagId)
        {
            return SingleOrDefault(c => c.Id.Equals(tagId));
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return FindAll()
                .OrderBy(pb => pb.Name);
        }

        public TagExtended GetTagWithDetails(int id)
        {
            return new TagExtended(GetTagById(id))
            {
                TagTrips = RepositoryContext.TagTrips
                .Where(tt => tt.TagId == id),

                TagPostBlogs = RepositoryContext.TagPostBlogs
                .Where(tpb => tpb.TagId == id)
            };
        }
    }
}

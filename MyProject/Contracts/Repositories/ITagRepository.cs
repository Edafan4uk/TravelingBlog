using Entities.Models;
using Entities.ExtendedModels;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        IEnumerable<Tag> GetAllTags();
        Tag GetTagById(int tagId);
        TagExtended GetTagWithDetails(int tagId);
    }
}

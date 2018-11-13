using System.ComponentModel.DataAnnotations.Schema;
using Entities.ExtendedModels;

namespace Entities.Models
{
    [Table("TagPostBlog")]
    public class TagPostBlog
    {
        public int TagId { get; set; }
        public int PostBlogId { get; set; }
        public TagExtended Tag { get; set; }
        public PostBlogExtended PostBlog { get; set; }
    }
}

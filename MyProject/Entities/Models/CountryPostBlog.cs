using System.ComponentModel.DataAnnotations.Schema;
using Entities.ExtendedModels;

namespace Entities.Models
{
    [Table("CountryPostBlog")]
    public class CountryPostBlog
    {
        public int CountryId { get; set; }
        public CountryExtended Country { get; set; }
        public int PostBlogId { get; set; }
        public PostBlogExtended PostBlog { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.ExtendedModels;

namespace Entities.Models
{
    [Table("Image")]
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int PostBlogId { get; set; }
        public PostBlogExtended PostBlog { get; set; }
    }
}

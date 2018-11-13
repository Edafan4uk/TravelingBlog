using System.ComponentModel.DataAnnotations.Schema;
using Entities.ExtendedModels;

namespace Entities.Models
{
    [Table("Purchase")]
    public class Purchase
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double AmountSpent { get; set; }
        public int PostBlogId { get; set; }
        public PostBlogExtended PostBlog { get; set; }
        public int CurrencyId { get; set; }
        public CurrencyExtended Currency { get; set; }
    }
}
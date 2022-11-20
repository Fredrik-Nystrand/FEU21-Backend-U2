using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Uppgift_2.Models
{
    public class ProductEntity
    {
        [Key]
        public string id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public string PartitionKey { get; set; } = "Products";
        public string ArtNo { get; set; } = null!;
        public string CategoryId { get; set; }
        public IEnumerable<TechSpec> TechnicalSpecifications { get; set; }
        public CategoryEntity Category { get; set; } = null;
    }
}

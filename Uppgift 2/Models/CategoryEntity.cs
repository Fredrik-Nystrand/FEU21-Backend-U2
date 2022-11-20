using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Uppgift_2.Models
{
    public class CategoryEntity
    {
        [Key]
        public string id { get; set; }
        public string Name { get; set; } = null!;
        public string PartitionKey { get; set; } = "Categories";

        public IEnumerable<ProductEntity> Products { get; set; }
    }
}

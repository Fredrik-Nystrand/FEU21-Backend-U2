namespace Uppgift_2.Models
{
    public class ProductUpdateRequest
    {
        public string id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public string PartitionKey { get; set; } = "Products";
        public string ArtNo { get; set; } = null!;
        public string CategoryId { get; set; }
        public IEnumerable<TechSpec> TechnicalSpecifications { get; set; }
    }
}

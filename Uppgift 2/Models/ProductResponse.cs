namespace Uppgift_2.Models
{
    public class ProductResponse
    {
        public string id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public string ArtNo { get; set; } = null!;
        public CategoryResponse Category { get; set; }
        public IEnumerable<TechSpec> TechnicalSpecifications { get; set; }
    }
}

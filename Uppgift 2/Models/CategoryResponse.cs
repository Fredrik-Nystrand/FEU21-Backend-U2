namespace Uppgift_2.Models
{
    public class CategoryResponse
    {
        public string id { get; set; }
        public string Name { get; set; } = null!;
        public IEnumerable<ProductResponse> Products { get; set; }
    }
}

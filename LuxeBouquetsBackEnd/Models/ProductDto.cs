namespace LuxeBouquetsBackEnd.Models
{
    public class ProductDto
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public required string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string? SubCategory { get; set; }
        public string? Description { get; set; }
    }
}
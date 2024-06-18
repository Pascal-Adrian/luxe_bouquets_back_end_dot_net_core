namespace LuxeBouquetsBackEnd.Models
{
    public class SubscribtionPlanDto
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public bool FreeDelivery { get; set; }
        public required string[] Details { get; set; }
        public int Savings { get; set; }
        public required string ImageUrl { get; set; }
    }
}
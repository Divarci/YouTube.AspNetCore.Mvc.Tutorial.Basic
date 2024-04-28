namespace YouTube.AspNetCore.Tutorial.Basic.Models.Entity
{
    public class ProductFeature
    {
        public int Id { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string? Colour { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

namespace YouTube.AspNetCore.Tutorial.Basic.Models.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ProductFeature? ProductFeature { get; set; }

    }
}

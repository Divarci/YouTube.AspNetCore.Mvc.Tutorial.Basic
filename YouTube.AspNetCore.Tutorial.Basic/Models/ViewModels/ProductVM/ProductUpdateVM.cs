using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ProductFeatureVM;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ProductVM
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public ProductFeatureUpdateVM? ProductFeature { get; set; }
    }
}

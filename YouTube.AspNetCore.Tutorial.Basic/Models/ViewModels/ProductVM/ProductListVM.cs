using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.CategoryVM;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ProductFeatureVM;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ProductVM
{
    public class ProductListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public CategoryListVM Category { get; set; }
        public ProductFeatureListVM? ProductFeature { get; set; }
    }
}

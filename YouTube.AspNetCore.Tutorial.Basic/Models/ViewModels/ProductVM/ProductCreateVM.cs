using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.Models.ValidationModels.ProductValidation;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ProductFeatureVM;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ProductVM
{
    [ModelMetadataType(typeof(ProductCreateValidation))]
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public int CategoryId { get; set; }

        public ProductFeatureCreateVM? ProductFeature { get; set; }
    }
}

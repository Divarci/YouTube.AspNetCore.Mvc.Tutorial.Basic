using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ValidationModels.CategoryValidation;
using YouTube.AspNetCore.Tutorial.Basic.Models.ValidationModels.ProductFeatureValidation;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ProductFeatureVM
{
    [ModelMetadataType(typeof(ProductFeatureCreateValidation))]
    public class ProductFeatureCreateVM
    {
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string? Colour { get; set; }
    }
}

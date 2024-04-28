using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ValidationModels.ProductFeatureValidation;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ProductFeatureVM
{
    [ModelMetadataType(typeof(ProductFeatureUpdateValidation))]
    public class ProductFeatureUpdateVM
    {
        public int Id { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string? Colour { get; set; }
        public int ProductId { get; set; }
    }
}

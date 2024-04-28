using System.ComponentModel.DataAnnotations;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.ValidationModels.ProductFeatureValidation
{
    public class ProductFeatureUpdateValidation
    {
        [MaxLength(20, ErrorMessage = "Please use maximum 20 characters")]
        public string? Colour { get; set; }
    }
}

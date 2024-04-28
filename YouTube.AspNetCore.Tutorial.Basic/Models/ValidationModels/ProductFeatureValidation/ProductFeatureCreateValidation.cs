using System.ComponentModel.DataAnnotations;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.ValidationModels.ProductFeatureValidation
{
    public class ProductFeatureCreateValidation
    {
        [MaxLength(20,ErrorMessage = "Please use maximum 20 characters")]
        public string? Colour { get; set; }
    }
}

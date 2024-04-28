using System.ComponentModel.DataAnnotations;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.ValidationModels.ProductValidation
{
    public class ProductUpdateValidation
    {
        [Required(ErrorMessage = "Please enter Product Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a valid Quantity")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Please enter a valid Product Price")]
        public decimal Price { get; set; }
    }
}

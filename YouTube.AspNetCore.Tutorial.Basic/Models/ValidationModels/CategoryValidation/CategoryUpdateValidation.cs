using System.ComponentModel.DataAnnotations;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.ValidationModels.CategoryValidation
{
    public class CategoryUpdateValidation
    {
        [MaxLength(25, ErrorMessage = "Maximum 25 characters are allowed!")]
        [Required(ErrorMessage = "Please enter Category Name")]
        public string Name { get; set; }
    }
}

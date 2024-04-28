using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.Models.ValidationModels.CategoryValidation;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.CategoryVM
{
    [ModelMetadataType(typeof(CategoryUpdateValidation))]
    public class CategoryUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

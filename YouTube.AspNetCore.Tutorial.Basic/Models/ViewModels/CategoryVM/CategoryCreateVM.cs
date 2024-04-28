using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.Models.ValidationModels.CategoryValidation;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.CategoryVM
{
    [ModelMetadataType(typeof(CategoryCreateValidation))]
    public class CategoryCreateVM
    {
        public string Name { get; set; }
    }
}

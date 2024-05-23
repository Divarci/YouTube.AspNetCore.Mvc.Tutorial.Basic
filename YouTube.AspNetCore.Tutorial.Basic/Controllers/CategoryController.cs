using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.Filters;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.CategoryVM;
using YouTube.AspNetCore.Tutorial.Basic.Services;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IGenericService<Category, CategoryListVM, CategoryCreateVM, CategoryUpdateVM> _repository;

        public CategoryController(IGenericService<Category, CategoryListVM, CategoryCreateVM, CategoryUpdateVM> repository)
        {
            _repository = repository;
        }

        public IActionResult GetAllCategories()
        {
            var categories = _repository.GetAllItems();
            return View(categories);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            _repository.CreateItem(request);
            return RedirectToAction("GetAllCategories", "Category");
        }

        [ServiceFilter(typeof(ParameterCheckFilter<Category, CategoryUpdateVM>))]
        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var item = _repository.GetItemById(id);
            return View(item);
        }

        [ServiceFilter(typeof(ParameterCheckFilter<Category, CategoryUpdateVM>))]
        [HttpPost]
        public IActionResult UpdateCategory(CategoryUpdateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            _repository.UpdateItem(request);
            return RedirectToAction("GetAllCategories", "Category");
        }

        [ServiceFilter(typeof(ParameterCheckFilter<Category, CategoryUpdateVM>))]
        public IActionResult DeleteCategory(int id)
        {
            _repository.DeleteItem(id);
            return RedirectToAction("GetAllCategories", "Category");
        }
    }
}

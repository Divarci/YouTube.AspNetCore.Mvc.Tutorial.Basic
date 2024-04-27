using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.CategoryVM;
using YouTube.AspNetCore.Tutorial.Basic.Services;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IGenericService<Category,CategoryListVM,CategoryCreateVM,CategoryUpdateVM> _repository;

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
            _repository.CreateItem(request);
            return RedirectToAction("GetAllCategories", "Category");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var item = _repository.GetItemById(id);
            return View(item);
        }

        [HttpPost]
        public IActionResult UpdateCategory(CategoryUpdateVM request)
        {
            _repository.UpdateItem(request);
            return RedirectToAction("GetAllCategories", "Category");
        }

        public IActionResult DeleteCategory(int id)
        {
            _repository.DeleteItem(id);
            return RedirectToAction("GetAllCategories", "Category");
        }
    }
}

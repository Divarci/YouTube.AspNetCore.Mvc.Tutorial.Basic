using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.MapperApp;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.CategoryVM;
using YouTube.AspNetCore.Tutorial.Basic.Services;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IGenericService<Category> _repository;
        private readonly IMapper<Category, CategoryListVM> _listMapper;
        private readonly IMapper<Category, CategoryCreateVM> _CreateMapper;
        private readonly IMapper<Category, CategoryUpdateVM> _UpdateMapper;

        public CategoryController(IGenericService<Category> repository, IMapper<Category, CategoryListVM> listMapper, IMapper<Category, CategoryCreateVM> createMapper, IMapper<Category, CategoryUpdateVM> updateMapper)
        {
            _repository = repository;
            _listMapper = listMapper;
            _CreateMapper = createMapper;
            _UpdateMapper = updateMapper;
        }

        public IActionResult GetAllCategories()
        {
            var categories = _repository.GetAllItems();
            var mappedCategoryList = _listMapper.Map<IList<Category>,List<CategoryListVM>>(categories);
            return View(mappedCategoryList);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryCreateVM request)
        {
            var category = _CreateMapper.Map<CategoryCreateVM,Category>(request);
            _repository.CreateItem(category);
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
            var category = _UpdateMapper.Map<CategoryUpdateVM, Category>(request);
            _repository.UpdateItem(category);
            return RedirectToAction("GetAllCategories", "Category");
        }

        public IActionResult DeleteCategory(int id)
        {
            _repository.DeleteItem(id);
            return RedirectToAction("GetAllCategories", "Category");
        }
    }
}

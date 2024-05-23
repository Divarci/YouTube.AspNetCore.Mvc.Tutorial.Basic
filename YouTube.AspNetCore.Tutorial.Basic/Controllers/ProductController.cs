using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.Filters;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.CategoryVM;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ProductVM;
using YouTube.AspNetCore.Tutorial.Basic.Services;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IGenericService<Product, ProductListVM,ProductCreateVM,ProductUpdateVM> _repository;
        private readonly IGenericService<Category,CategoryListVM,CategoryCreateVM,CategoryUpdateVM> _categoryService;

        public ProductController(IGenericService<Product, ProductListVM, ProductCreateVM, ProductUpdateVM> repository, IGenericService<Category, CategoryListVM, CategoryCreateVM, CategoryUpdateVM> categoryService)
        {
            _repository = repository;
            _categoryService = categoryService;
        }

        public IActionResult GetAllProducts()
        {
            var products = _repository.GetAllItems(x=>x.Category);
            return View(products);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            var categories = _categoryService.GetAllItems();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                var categories = _categoryService.GetAllItems();
                ViewBag.Categories = categories;
                return View(request);
            }
            _repository.CreateItem(request);
            return RedirectToAction("GetAllProducts", "Product");
        }

        [ServiceFilter(typeof(ParameterCheckFilter<Category, ProductUpdateVM>))]
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            var categories = _categoryService.GetAllItems();
            ViewBag.Categories = categories;
            var product = _repository.GetItemById(id);
            return View(product);
        }

        [ServiceFilter(typeof(ParameterCheckFilter<Category, ProductUpdateVM>))]
        [HttpPost]
        public IActionResult UpdateProduct(ProductUpdateVM request)
        {
            if (!ModelState.IsValid)
            {
                var categories = _categoryService.GetAllItems();
                ViewBag.Categories = categories;
                return View(request);
            }
            _repository.UpdateItem(request);
            return RedirectToAction("GetAllProducts", "Product");
        }

        [ServiceFilter(typeof(ParameterCheckFilter<Category, ProductUpdateVM>))]
        public IActionResult DeleteProduct(int id)
        {
            _repository.DeleteItem(id);
            return RedirectToAction("GetAllProducts", "Product");
        }
    }
}

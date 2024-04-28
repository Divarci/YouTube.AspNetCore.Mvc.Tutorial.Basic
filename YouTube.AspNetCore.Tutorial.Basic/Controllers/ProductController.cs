using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ProductVM;
using YouTube.AspNetCore.Tutorial.Basic.Services;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    public class ProductController : Controller
    {
        private readonly IGenericService<Product, ProductListVM,ProductCreateVM,ProductUpdateVM> _repository;

        public ProductController(IGenericService<Product, ProductListVM, ProductCreateVM, ProductUpdateVM> repository)
        {
            _repository = repository;
        }

        public IActionResult GetAllProducts()
        {
            var products = _repository.GetAllItems(x=>x.Category);
            return View(products);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductCreateVM request)
        {
            _repository.CreateItem(request);
            return RedirectToAction("GetAllProducts", "Product");
        }

        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            var product = _repository.GetItemById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult UpdateProduct(ProductUpdateVM request)
        {
            _repository.UpdateItem(request);
            return RedirectToAction("GetAllProducts", "Product");
        }

        public IActionResult DeleteProduct(int id)
        {
            _repository.DeleteItem(id);
            return RedirectToAction("GetAllProducts", "Product");
        }
    }
}

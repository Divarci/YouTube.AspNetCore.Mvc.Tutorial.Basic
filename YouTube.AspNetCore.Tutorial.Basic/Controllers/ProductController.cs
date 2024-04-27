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
            var products = _repository.GetAllItems(x=>x.Category,x=>x.ProductFeature!);
            return View(products);
        }
    }
}

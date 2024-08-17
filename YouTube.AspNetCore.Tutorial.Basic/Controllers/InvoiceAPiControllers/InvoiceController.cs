using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceDto;
using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceItemsDto;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ProductVM;
using YouTube.AspNetCore.Tutorial.Basic.Services;
using YouTube.AspNetCore.Tutorial.Basic.Services.InvoiceApiServices.InvoiceService;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers.InvoiceAPiControllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IGenericService<Product, ProductListVM, ProductCreateVM, ProductUpdateVM> _productService;
        private readonly IMemoryCache _memoeryCache;

        private const string InvoiceCacheKey = "InvoiceDraftList";

        public InvoiceController(IInvoiceService invoiceService, IGenericService<Product, ProductListVM, ProductCreateVM, ProductUpdateVM> productService, IMemoryCache memoeryCache)
        {
            _invoiceService = invoiceService;
            _productService = productService;
            _memoeryCache = memoeryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoicesForCompany(int clientId)
        {
            var result = await _invoiceService.GetAllInvoicesByCompanyIdAsync(clientId);
            return View(result);
        }

        [HttpGet]
        public IActionResult CreateInvoiceForCompany(int clientId)
        {            
            ViewBag.ProductList = _productService.GetAllItems(x=>x.Category).ToList();

            //check true false oprions
            var result = _memoeryCache.TryGetValue(InvoiceCacheKey, out InvoiceCreateDto? cachedInvoice);
            if (cachedInvoice is null)
                cachedInvoice = new() { ClientId = clientId, InvoiceItems = [] };

            return View(cachedInvoice);
        }

        [HttpGet]
        public IActionResult AddproductToList(int productId, int quantity, int clientId, string poNumber)
        {
            var product = _productService.GetAllItems(x=>x.Category).FirstOrDefault(x=>x.Id == productId);
            if (product is null)
            {
                //throw error
            }

            var invoiceItem = new InvoiceItemCreateDto
            {
                Name = product.Name,
                Price = product.Price,
                Quantity = quantity,
            };

            var result = _memoeryCache.TryGetValue(InvoiceCacheKey, out InvoiceCreateDto? cachedInvoice);
            if (cachedInvoice is null)
                cachedInvoice = new() { ClientId = clientId, InvoiceItems = [] };

            cachedInvoice.InvoiceItems.Add(invoiceItem);
            cachedInvoice.PONumber = poNumber;

            _memoeryCache.Set(InvoiceCacheKey, cachedInvoice, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            return Json(true);
        }

        [HttpGet]
        public IActionResult RemoveProductFromList(int index, int clientId, string poNumber)
        {
            var result = _memoeryCache.TryGetValue(InvoiceCacheKey, out InvoiceCreateDto? cachedInvoice);
            if (cachedInvoice is null)
                throw new Exception();

            cachedInvoice.InvoiceItems.RemoveAt(index);
            cachedInvoice.PONumber = poNumber;

            _memoeryCache.Set(InvoiceCacheKey, cachedInvoice, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            return Json(true);
        }
        


    }
}

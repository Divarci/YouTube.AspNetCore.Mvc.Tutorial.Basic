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
        public async Task<IActionResult> GetAllInvoicesForCompany(int id)
        {
            var result = await _invoiceService.GetAllInvoicesByCompanyIdAsync(id);
            return View(result);
        }

        [HttpGet]
        public IActionResult CreateInvoiceForCompany(int id)
        {            
            ViewBag.ProductList = _productService.GetAllItems(x=>x.Category).ToList();

            //check true false oprions
            var result = _memoeryCache.TryGetValue(InvoiceCacheKey, out InvoiceCreateDto? cachedInvoice);
            if (cachedInvoice is null)
                cachedInvoice = new() { ClientId = id, InvoiceItems = [] };

            return View(cachedInvoice);
        }

        [HttpGet]
        public IActionResult AddproductToList(int productId, int quantity, int id, string poNumber)
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
                cachedInvoice = new() { ClientId = id, InvoiceItems = [] };

            cachedInvoice.InvoiceItems.Add(invoiceItem);
            cachedInvoice.PONumber = poNumber;
            cachedInvoice.ClientId = id;

            _memoeryCache.Set(InvoiceCacheKey, cachedInvoice, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });           

            return Json(cachedInvoice);
        }

        [HttpPost]
        public async Task<IActionResult> SendInvoiceToApi(InvoiceCreateDto request)
        {
            request.InvoiceDate = DateTime.Now;
            await _invoiceService.CreateInvoiceAsync(request);
            _memoeryCache.Remove(InvoiceCacheKey);
            return RedirectToAction("GetAllInvoicesForCompany", "Invoice", new { id = request.ClientId });
        }

        [HttpGet]
        public async Task<IActionResult> ViewOrEditInvoice(int id, bool isView)
        {
            InvoiceUpdateForRemoveItemsDto? response = null;
            var result = _memoeryCache.TryGetValue("invoiceEditOrView", out InvoiceUpdateForRemoveItemsDto? cachedInvoice);
            if (cachedInvoice is null)
            {
                response = await _invoiceService.GetInvoiceByIdAsync(id);
                _memoeryCache.Set("invoiceEditOrView", response, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
            }
            ViewBag.IsView = isView;
            ViewBag.ProductList = _productService.GetAllItems(x => x.Category).ToList();

            return View(cachedInvoice ?? response);            
        }

        [HttpGet]
        public IActionResult AddproductToListForUpdate(int productId, int quantity, int id, string poNumber)
        {
            var product = _productService.GetAllItems(x => x.Category).FirstOrDefault(x => x.Id == productId);
            if (product is null)
            {
                //throw error
            }

            var invoiceItem = new InvoiceItemUpdateDto
            {
                Name = product.Name,
                Price = product.Price,
                Quantity = quantity,
            };

            var result = _memoeryCache.TryGetValue("invoiceEditOrView", out InvoiceUpdateForRemoveItemsDto? cachedInvoice);
            if (cachedInvoice is null)
                throw new Exception();

            cachedInvoice.InvoiceItems.Add(invoiceItem);
            cachedInvoice.PONumber = poNumber;
            cachedInvoice.ClientId = id;

            _memoeryCache.Set("invoiceEditOrView", cachedInvoice, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            return Json(cachedInvoice);
        }

        [HttpPost]
        public async Task<IActionResult> SendInvoiceToApiForUpdate(InvoiceUpdateForRemoveItemsDto request)
        {
            request.InvoiceDate = DateTime.Now;
            await _invoiceService.UpdateInvoiceAsync(request);
            _memoeryCache.Remove("invoiceEditOrView");
            return RedirectToAction("GetAllInvoicesForCompany", "Invoice", new { request.ClientId });
        }

        [HttpGet]
        public IActionResult RemoveProductFromList(int index)
        {
            var result = _memoeryCache.TryGetValue(InvoiceCacheKey, out InvoiceCreateDto? cachedInvoice);
            if (cachedInvoice is null)
                throw new Exception();

            cachedInvoice.InvoiceItems.RemoveAt(index);
            _memoeryCache.Set(InvoiceCacheKey, cachedInvoice, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            return RedirectToAction("CreateInvoiceForCompany", "Invoice",new {cachedInvoice.ClientId});
        }

        [HttpGet]
        public IActionResult RemoveProductFromUpdateList(int index, string poNumber)
        {
            var result = _memoeryCache.TryGetValue("invoiceEditOrView", out InvoiceUpdateForRemoveItemsDto? cachedInvoice);
            if (cachedInvoice is null)
                throw new Exception();

            cachedInvoice.InvoiceItems.RemoveAt(index);
            cachedInvoice.PONumber = poNumber;
            _memoeryCache.Set(InvoiceCacheKey, cachedInvoice, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            return Json(cachedInvoice);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveInvoice(int id, int clientId)
        {
            await _invoiceService.RemoveInvoiceAsync(id);
            return RedirectToAction("GetAllInvoicesForCompany", "Invoice", new { id = clientId });
        }

        public IActionResult CancelAction(int id,string cacheKey)
        {
            _memoeryCache.Remove(cacheKey);
            return RedirectToAction("GetAllInvoicesForCompany", "Invoice", new { id = id });
        }
    }
}

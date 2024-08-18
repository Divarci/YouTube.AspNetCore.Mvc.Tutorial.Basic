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
            ViewBag.ProductList = _productService.GetAllItems(x => x.Category).ToList();
            string cacheKey = $"InvoiceCreateDraft-{clientId}";
            //check true false oprions
            var result = _memoeryCache.TryGetValue(cacheKey, out InvoiceCreateDto? cachedInvoice);
            if (cachedInvoice is null)
                cachedInvoice = new() { ClientId = clientId, InvoiceItems = [] };

            return View(cachedInvoice);
        }

        [HttpGet]
        public IActionResult AddproductToList(int productId, int quantity, int clientId, string poNumber)
        {
            var product = _productService.GetAllItems(x => x.Category).FirstOrDefault(x => x.Id == productId);
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

            string cacheKey = $"InvoiceCreateDraft-{clientId}";

            var result = _memoeryCache.TryGetValue(cacheKey, out InvoiceCreateDto? cachedInvoice);
            if (cachedInvoice is null)
                cachedInvoice = new() { ClientId = clientId, InvoiceItems = [] };

            cachedInvoice.InvoiceItems.Add(invoiceItem);
            cachedInvoice.PONumber = poNumber;

            _memoeryCache.Set(cacheKey, cachedInvoice, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            return Json(true);
        }

        [HttpGet]
        public IActionResult RemoveProductFromList(int index, int clientId, string poNumber)
        {
            string cacheKey = $"InvoiceCreateDraft-{clientId}";
            var result = _memoeryCache.TryGetValue(cacheKey, out InvoiceCreateDto? cachedInvoice);
            if (cachedInvoice is null)
                throw new Exception();

            cachedInvoice.InvoiceItems.RemoveAt(index);
            cachedInvoice.PONumber = poNumber;

            _memoeryCache.Set(cacheKey, cachedInvoice, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> SendInvoiceToApi(string cacheKey)
        {
            var result = _memoeryCache.TryGetValue(cacheKey, out InvoiceCreateDto? cachedInvoice);
            if (!result || cachedInvoice is null)
                throw new Exception();

            cachedInvoice.InvoiceDate = DateTime.Now;
            var resultApiRequest = await _invoiceService.CreateInvoiceAsync(cachedInvoice); // we can return a success message

            _memoeryCache.Remove(cacheKey);
            return RedirectToAction("GetAllInvoicesForCompany", "Invoice", new { clientId = cachedInvoice.ClientId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDraftInvoice(string cacheKey, int clientId)
        {
            _memoeryCache.Remove(cacheKey);

            var invoices = await _invoiceService.GetAllInvoicesByCompanyIdAsync(clientId);
            if (invoices.Count == 0)
                return RedirectToAction("GetAllClients", "Client");

            return RedirectToAction("GetAllInvoicesForCompany", "Invoice", new { clientId });
        }

        [HttpGet]
        public async Task<IActionResult> ViewEditInvoiceForCompany(int id, string invoiceStatus)
        {
            if (invoiceStatus == "Edit")
            {
                ViewBag.ProductList = _productService.GetAllItems(x => x.Category).ToList();
                ViewBag.Action = invoiceStatus;
                string cacheKey = $"InvoiceUpdateDraft-{id}";

                var result = _memoeryCache.TryGetValue(cacheKey, out InvoiceUpdateForRemoveItemsDto? cachedInvoice);
                if (cachedInvoice is null)
                    cachedInvoice = await _invoiceService.GetInvoiceByIdAsync(id);

                return View(cachedInvoice);
            }

            var viewData = await _invoiceService.GetInvoiceByIdAsync(id);
            ViewBag.Action = invoiceStatus;
            return View(viewData);

        }

        [HttpGet]
        public async Task<IActionResult> AddProductToListForUpdate(int productId, int quantity, int clientId, string poNumber, int invoiceId)
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

            string cacheKey = $"InvoiceUpdateDraft-{invoiceId}";

            var result = _memoeryCache.TryGetValue(cacheKey, out InvoiceUpdateForRemoveItemsDto? cachedInvoice);
            if (cachedInvoice is null)
                cachedInvoice = await _invoiceService.GetInvoiceByIdAsync(invoiceId);



            cachedInvoice.InvoiceItems.Add(invoiceItem);
            cachedInvoice.PONumber = poNumber;

            _memoeryCache.Set(cacheKey, cachedInvoice, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveProductFromListForUpdate(int index, int clientId, string poNumber, int invoiceId)
        {
            string cacheKey = $"InvoiceUpdateDraft-{invoiceId}";
            var result = _memoeryCache.TryGetValue(cacheKey, out InvoiceUpdateForRemoveItemsDto? cachedInvoice);
            if (cachedInvoice is null)
                cachedInvoice = await _invoiceService.GetInvoiceByIdAsync(invoiceId);

            cachedInvoice.InvoiceItems.RemoveAt(index);
            cachedInvoice.PONumber = poNumber;

            _memoeryCache.Set(cacheKey, cachedInvoice, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            return Json(true);
        }


        [HttpGet]
        public async Task<IActionResult> SendInvoiceToApiForUpdate(string cacheKey)
        {
            var result = _memoeryCache.TryGetValue(cacheKey, out InvoiceUpdateForRemoveItemsDto? cachedInvoice);
            if (!result || cachedInvoice is null)
                throw new Exception();

            await _invoiceService.UpdateInvoiceAsync(cachedInvoice); // we can return a success message

            _memoeryCache.Remove(cacheKey);
            return RedirectToAction("GetAllInvoicesForCompany", "Invoice", new { clientId = cachedInvoice.ClientId });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveInvoice(int id, int clientId)
        {
            await _invoiceService.RemoveInvoiceAsync(id);
            var invoices = await _invoiceService.GetAllInvoicesByCompanyIdAsync(clientId);
            if(invoices.Count == 0)
                return RedirectToAction("GetAllClients", "Client");

            return RedirectToAction("GetAllInvoicesForCompany", "Invoice", new { clientId });
        }
    }
}

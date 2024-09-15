using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceDto;
using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceItemsDto;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ProductVM;
using YouTube.AspNetCore.Tutorial.Basic.Services;
using YouTube.AspNetCore.Tutorial.Basic.Services.InvoiceApiServices.CachedOperationsService;
using YouTube.AspNetCore.Tutorial.Basic.Services.InvoiceApiServices.InvoiceService;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers.InvoiceAPiControllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IGenericService<Product, ProductListVM, ProductCreateVM, ProductUpdateVM> _productService;
        private readonly ICachedOperationsService _cachedData;

        public InvoiceController(IInvoiceService invoiceService, IGenericService<Product, ProductListVM, ProductCreateVM, ProductUpdateVM> productService, ICachedOperationsService cachedData)
        {
            _invoiceService = invoiceService;
            _productService = productService;
            _cachedData = cachedData;
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
            var cachedInvoice = _cachedData.CreateInvoiceForCompany(clientId);
            return View(cachedInvoice);
        }

        [HttpGet]
        public IActionResult AddproductToList(int productId, int quantity, int clientId, string poNumber)
        {
            var result = _cachedData.AddProductToList(productId, quantity, clientId, poNumber);
            return Json(result);
        }

        [HttpGet]
        public IActionResult RemoveProductFromList(int index, int clientId, string poNumber)
        {
            var result = _cachedData.RemoveProductFromList(index, clientId, poNumber);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> SendInvoiceToApi(string cacheKey)
        {
            var result = await _cachedData.SendInvoiceToApiAsync(cacheKey);
            // we can return a success message add toastr message result.message
            return RedirectToAction("GetAllInvoicesForCompany", "Invoice", new { result.clientId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDraftInvoice(string cacheKey, int clientId)
        {
            var result = _cachedData.DeleteDraftInvoice(cacheKey, clientId);

            var invoices = await _invoiceService.GetAllInvoicesByCompanyIdAsync(clientId);

            // we can return a success message add toastr message result

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

                var cachedInvoice = _cachedData.ViewEditInvoiceForCompanyAsync(id);             

                return View(cachedInvoice);
            }

            var viewData = await _invoiceService.GetInvoiceByIdAsync(id);
            ViewBag.Action = invoiceStatus;
            return View(viewData);
        }

        [HttpGet]
        public async Task<IActionResult> AddProductToListForUpdate(int productId, int quantity, int clientId, string poNumber, int invoiceId)
        {
            var result = await _cachedData.AddProductToListForUpdateAsync(productId, quantity, clientId, poNumber, invoiceId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveProductFromListForUpdate(int index, int clientId, string poNumber, int invoiceId)
        {
            var result = await _cachedData.RemoveProductFromListForUpdateAsync(index, clientId, poNumber, invoiceId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> SendInvoiceToApiForUpdate(string cacheKey)
        {
            var clientId = await _cachedData.SendInvoiceToApiAsync(cacheKey);
            // we can return a success message
            return RedirectToAction("GetAllInvoicesForCompany", "Invoice", new { clientId });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveInvoice(int id, int clientId)
        {
            await _invoiceService.RemoveInvoiceAsync(id);

            var invoices = await _invoiceService.GetAllInvoicesByCompanyIdAsync(clientId);
            if (invoices.Count == 0)
                return RedirectToAction("GetAllClients", "Client");

            return RedirectToAction("GetAllInvoicesForCompany", "Invoice", new { clientId });
        }
    }
}

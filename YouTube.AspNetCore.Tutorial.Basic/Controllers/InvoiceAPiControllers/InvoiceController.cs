using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.Services.InvoiceApiServices.InvoiceService;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers.InvoiceAPiControllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public async Task<IActionResult> GetAllInvoicesForCompany(int id)
        {
            var result = await _invoiceService.GetAllInvoicesByCompanyIdAsync(id);
            return View(result);
        }
    }
}

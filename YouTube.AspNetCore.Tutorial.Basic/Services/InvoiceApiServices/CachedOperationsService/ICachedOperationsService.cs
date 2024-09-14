using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceDto;

namespace YouTube.AspNetCore.Tutorial.Basic.Services.InvoiceApiServices.CachedOperationsService;

public interface ICachedOperationsService
{
    InvoiceCreateDto CreateInvoiceForCompany(int clientId);
    string AddProductToList(int productId, int quantity, int clientId, string poNumber);
    public string RemoveProductFromList(int index, int clientId, string poNumber);
    Task<(string message, int clientId)> SendInvoiceToApiAsync(string cacheKey);
    string DeleteDraftInvoice(string cacheKey, int clientId);
}

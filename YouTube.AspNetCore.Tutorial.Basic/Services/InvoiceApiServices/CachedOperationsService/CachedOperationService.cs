using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using YouTube.AspNetCore.Tutorial.Basic.Exceptions;
using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceDto;
using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceItemsDto;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ProductVM;
using YouTube.AspNetCore.Tutorial.Basic.Services.InvoiceApiServices.CachedOperationsService;
using YouTube.AspNetCore.Tutorial.Basic.Services.InvoiceApiServices.InvoiceService;

namespace YouTube.AspNetCore.Tutorial.Basic.Services.InvoiceApiServices.CachedOperationsServices;

public class CachedOperationService : ICachedOperationsService
{
    private readonly IInvoiceService _invoiceService;
    private readonly IGenericService<Product, ProductListVM, ProductCreateVM, ProductUpdateVM> _productService;
    private readonly IMemoryCache _memoryCache;

    public CachedOperationService(IInvoiceService invoiceService, IGenericService<Product, ProductListVM, ProductCreateVM, ProductUpdateVM> productService, IMemoryCache memoryCache)
    {
        _invoiceService = invoiceService;
        _productService = productService;
        _memoryCache = memoryCache;
    }

    public InvoiceCreateDto CreateInvoiceForCompany(int clientId)
    {
        string cacheKey = $"InvoiceCreateDraft-{clientId}";

        _memoryCache.TryGetValue(cacheKey, out InvoiceCreateDto? cachedInvoice);

        cachedInvoice ??= new() { ClientId = clientId, InvoiceItems = [] };

        return cachedInvoice;
    }

    public string AddProductToList(int productId, int quantity, int clientId, string poNumber)
    {
        var product = _productService.GetAllItems(x => x.Category).FirstOrDefault(x => x.Id == productId);
        if (product == null)
            return "Product with given Id is not exist";

        var invoiceItem = new InvoiceItemCreateDto
        {
            Name = product.Name,
            Price = product.Price,
            Quantity = quantity,
        };

        string cacheKey = $"InvoiceCreateDraft-{clientId}";

        _memoryCache.TryGetValue(cacheKey, out InvoiceCreateDto? cachedInvoice);
        if (cachedInvoice is null)
            cachedInvoice = new() { ClientId = clientId, InvoiceItems = [] };

        cachedInvoice.InvoiceItems.Add(invoiceItem);
        cachedInvoice.PONumber = poNumber;

        _memoryCache.Set(cacheKey, cachedInvoice, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        });

        return string.Empty;
    }

    public string RemoveProductFromList(int index, int clientId, string poNumber)
    {
        string cacheKey = $"InvoiceCreateDraft-{clientId}";
        _memoryCache.TryGetValue(cacheKey, out InvoiceCreateDto? cachedInvoice);
        if (cachedInvoice is null)
            throw new ClientSideExceptions("Cached Invoice not found");

        cachedInvoice.InvoiceItems.RemoveAt(index);
        cachedInvoice.PONumber = poNumber;

        _memoryCache.Set(cacheKey, cachedInvoice, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        });

        return string.Empty;
    }

    public async Task<(string message,int clientId)> SendInvoiceToApiAsync(string cacheKey)
    {
        var result = _memoryCache.TryGetValue(cacheKey, out InvoiceCreateDto? cachedInvoice);
        if (!result || cachedInvoice is null)
            throw new Exception();

        cachedInvoice.InvoiceDate = DateTime.Now;
        var resultApiRequest = await _invoiceService.CreateInvoiceAsync(cachedInvoice);

        _memoryCache.Remove(cacheKey);

        return ($"The Invoice number: {resultApiRequest.PONumber} has been created successfully",
            resultApiRequest.ClientId);
    }

    public string DeleteDraftInvoice(string cacheKey, int clientId)
    {
        _memoryCache.Remove(cacheKey);
        return "Draft invoice is deleted";
    }

}

using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceDto;
using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceItemsDto;

namespace YouTube.AspNetCore.Tutorial.Basic.Services.InvoiceApiServices.InvoiceService
{
    public interface IInvoiceService
    {
        Task<List<InvoiceDto>> GetAllInvoicesByCompanyIdAsync(int id);
        Task<InvoiceUpdateForRemoveItemsDto> GetInvoiceByIdAsync(int id);
        Task<InvoiceCreateDto> CreateInvoiceAsync(InvoiceCreateDto request);
        Task UpdateInvoiceAsync(InvoiceUpdateForRemoveItemsDto request);
        Task RemoveInvoiceAsync(int id);


        Task RemoveInvoiceItemsAsync(RemoveInvoiceItemsDto request);
        Task<List<InvoiceItemCreateDto>> AddInvoiceItemsAsync(AddInvoiceItemsDto request);

    }
}

using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.ClientDto;
using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceItemsDto;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceDto
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string PONumber { get; set; }
        public decimal Vat { get; set; }
        public decimal Total { get; set; }
        public decimal GrandTotal { get; set; }

        public int ClientId { get; set; }
        public ClientDtoForInvoice Client { get; set; }

        public List<InvoiceItemDto> InvoiceItems { get; set; }
    }
}

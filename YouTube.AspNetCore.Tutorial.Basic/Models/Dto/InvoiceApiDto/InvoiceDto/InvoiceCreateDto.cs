using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceItemsDto;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceDto
{
    public class InvoiceCreateDto
    {
        public DateTime InvoiceDate { get; set; }
        public string PONumber { get; set; }
        //public decimal Vat { get => InvoiceItems.Sum(x => x.Vat); private set { } }
        //public decimal Total { get => InvoiceItems.Sum(x => x.Total); private set { } }
        //public decimal GrandTotal { get => InvoiceItems.Sum(x => x.GrandTotal); private set { } }
        public int ClientId { get; set; }

        public List<InvoiceItemCreateDto> InvoiceItems { get; set; }
    }
}

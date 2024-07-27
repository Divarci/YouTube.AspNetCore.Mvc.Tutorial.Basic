namespace YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceItemsDto
{
    public class AddInvoiceItemsDto
    {
        public int InvoiceId { get; set; }
        public List<InvoiceItemCreateDto> InvoiceItemList { get; set; }
    }
}

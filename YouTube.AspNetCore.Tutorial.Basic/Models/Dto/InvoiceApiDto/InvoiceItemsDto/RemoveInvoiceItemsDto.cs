namespace YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceItemsDto
{
    public class RemoveInvoiceItemsDto
    {
        public int InvoiceId { get; set; }
        public List<int> RemoveInvoiceItemIdList { get; set; }
    }
}

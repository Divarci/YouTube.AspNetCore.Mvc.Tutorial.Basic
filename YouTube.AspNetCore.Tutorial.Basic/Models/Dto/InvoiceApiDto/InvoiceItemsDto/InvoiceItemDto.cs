namespace YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceItemsDto
{
    public class InvoiceItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Vat { get; set; }
        public decimal Total { get; set; }
        public decimal GrandTotal { get; set; }
    }
}

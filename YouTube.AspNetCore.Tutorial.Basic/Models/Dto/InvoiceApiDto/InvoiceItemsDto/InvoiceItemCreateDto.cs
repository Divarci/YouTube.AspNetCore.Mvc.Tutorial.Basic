namespace YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceItemsDto
{
    public class InvoiceItemCreateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        //public decimal Vat { get => Total * 0.2m; private set { } }
        //public decimal Total { get => Price * Quantity; private set { } }
        //public decimal GrandTotal { get => Total + Vat; private set { } }
    }
}

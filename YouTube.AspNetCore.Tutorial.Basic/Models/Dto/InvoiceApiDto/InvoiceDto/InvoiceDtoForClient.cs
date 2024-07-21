namespace YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceDto
{
    public class InvoiceDtoForClient
    {
        public DateTime InvoiceDate { get; set; }
        public string PONumber { get; set; }
        public decimal GrandTotal { get; set; }
    }
}

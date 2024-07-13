namespace YouTube.AspNetCore.Tutorial.Basic.Models.InvoiceApiModels.InvoicesDto;
public class InvoiceDtoForClient
{
    public DateTime InvoiceDate { get; set; }
    public string PONumber { get; set; }
    public decimal GrandTotal { get; set; }
}
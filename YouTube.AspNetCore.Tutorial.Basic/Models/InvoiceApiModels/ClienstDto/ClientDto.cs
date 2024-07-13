using YouTube.AspNetCore.Tutorial.Basic.Models.InvoiceApiModels.InvoicesDto;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.InvoiceApiModels.ClienstDto;

public class ClientDto
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string Owner { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }

    public List<InvoiceDtoForClient> Invoices { get; set; }
}
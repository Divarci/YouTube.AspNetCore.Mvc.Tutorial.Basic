using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceItemsDto;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceDto
{
    public class InvoiceCreateDto
    {
        public DateTime InvoiceDate { get; set; }
        public string PONumber { get; set; }       
        public int ClientId { get; set; }

        public List<InvoiceItemCreateDto> InvoiceItems { get; set; }
    }
}

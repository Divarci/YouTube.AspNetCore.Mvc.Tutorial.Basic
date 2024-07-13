using YouTube.AspNetCore.Tutorial.Basic.Models.InvoiceApiModels.ClienstDto;
using YouTube.AspNetCore.Tutorial.Basic.Models.InvoiceApiModels.ResponseDto;

namespace YouTube.AspNetCore.Tutorial.Basic.Services.InvoiceApiServices.ClientServices;

public interface IClientServices
{
    Task<CustomResponseDto<List<ClientDto>>> GetAllClientsAsync();
}

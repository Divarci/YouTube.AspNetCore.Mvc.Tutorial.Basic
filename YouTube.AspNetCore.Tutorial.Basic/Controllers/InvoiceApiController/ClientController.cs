using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.Services.InvoiceApiServices.ClientServices;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers.InvoiceApiController;
public class ClientController : BaseController<ClientController>
{
    private readonly IClientServices _clientServices;

    public ClientController(IClientServices clientServices)
    {
        _clientServices = clientServices;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _clientServices.GetAllClientsAsync();
        return HandleAction(result);
    }
}

using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.ClientDto;
using YouTube.AspNetCore.Tutorial.Basic.Services.InvoiceApiServices.ClientService;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers.InvoiceAPiControllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IActionResult> GetAllClients()
        {
            var response = await _clientService.GetAllClientsAsync();
            return View(response);
        }

        [HttpGet]
        public IActionResult CreateClient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(ClientCreateDto request)
        {
            await _clientService.CreateClientAsync(request);
            return RedirectToAction("GetAllClients", "Client");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateClient(int id)
        {
            var response = await _clientService.GetClientByIdAsync(id);
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClient(ClientUpdateDto request)
        {
            await _clientService.UpdateClientAsync(request);
            return RedirectToAction("GetAllClients", "Client");
        }

        public async Task<IActionResult> RemoveClient(int id)
        {
            await _clientService.RemoveClientAsync(id);
            return RedirectToAction("GetAllClients", "Client");
        }
    }
}

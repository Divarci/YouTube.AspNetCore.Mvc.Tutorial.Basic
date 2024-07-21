using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.ClientDto;

namespace YouTube.AspNetCore.Tutorial.Basic.Services.InvoiceApiServices.ClientService
{
    public interface IClientService
    {
        Task<List<ClientDto>> GetAllClientsAsync();
        Task<ClientCreateDto> CreateClientAsync(ClientCreateDto request);
        Task<ClientUpdateDto> GetClientByIdAsync(int id);
        Task UpdateClientAsync(ClientUpdateDto request);
        Task RemoveClientAsync(int id);
    }
}

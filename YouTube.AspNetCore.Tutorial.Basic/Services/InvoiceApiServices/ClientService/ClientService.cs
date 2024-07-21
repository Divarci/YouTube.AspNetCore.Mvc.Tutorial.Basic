using Newtonsoft.Json;
using YouTube.AspNetCore.Tutorial.Basic.MapperApp;
using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.ClientDto;
using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.Response;

namespace YouTube.AspNetCore.Tutorial.Basic.Services.InvoiceApiServices.ClientService
{
    public class ClientService : IClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public ClientService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<List<ClientDto>> GetAllClientsAsync()
        {
            // baseaddress/api/client
            var response = await _httpClient.GetAsync("api/Client");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var content = await Convert<List<ClientDto>>(response);       
            return content!.Data!;
        }

        public async Task<ClientCreateDto> CreateClientAsync(ClientCreateDto request)
        {
            // baseaddress/api/client
            var response = await _httpClient.PostAsJsonAsync("api/Client", request);

            if (!response.IsSuccessStatusCode)
                throw new Exception();
            
            var content = await Convert<ClientCreateDto>(response);
            return content!.Data!;
        }

        public async Task<ClientUpdateDto> GetClientByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Client/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var content = await Convert<ClientDto>(response);
            var updateDto = _mapper.Map<ClientDto, ClientUpdateDto>(content!.Data!, 3);
            return updateDto;
        }

        public async Task UpdateClientAsync(ClientUpdateDto request)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Client", request);
            if (!response.IsSuccessStatusCode)
                throw new Exception();
        }

        public async Task RemoveClientAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Client/{id}");
            if (!response.IsSuccessStatusCode)
                throw new Exception();
        }


        private async Task<CustomResponseDto<T>?> Convert<T>(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<CustomResponseDto<T>>(responseContent);
            return content;
        }

    }
}

using Newtonsoft.Json;
using YouTube.AspNetCore.Tutorial.Basic.Models.InvoiceApiModels.ClienstDto;
using YouTube.AspNetCore.Tutorial.Basic.Models.InvoiceApiModels.ResponseDto;

namespace YouTube.AspNetCore.Tutorial.Basic.Services.InvoiceApiServices.ClientServices;

public class ClientServices : IClientServices
{
    private readonly HttpClient _httpClient;

    public ClientServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CustomResponseDto<List<ClientDto>>> GetAllClientsAsync()
    {
        var response =await _httpClient.GetAsync("https://localhost:7110/api/Client");
        if (!response.IsSuccessStatusCode)
            throw new Exception();

        var stringContent = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<CustomResponseDto<List<ClientDto>>>(stringContent);
        if (content is null)
            throw new Exception();

        return content;
    }
}

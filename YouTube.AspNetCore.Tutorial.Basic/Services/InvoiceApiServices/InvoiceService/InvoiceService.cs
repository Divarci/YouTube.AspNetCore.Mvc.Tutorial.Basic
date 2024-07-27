using Newtonsoft.Json;
using YouTube.AspNetCore.Tutorial.Basic.MapperApp;
using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceDto;
using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.InvoiceItemsDto;
using YouTube.AspNetCore.Tutorial.Basic.Models.Dto.Response;

namespace YouTube.AspNetCore.Tutorial.Basic.Services.InvoiceApiServices.InvoiceService
{
    public class InvoiceService : IInvoiceService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public InvoiceService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<List<InvoiceDto>> GetAllInvoicesByCompanyIdAsync(int id)
        {
            var response = await _httpClient.GetAsync("api/Invoice/GetallInvoices");
            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var content = await Convert<List<InvoiceDto>>(response);
            var data = content!.Data!.Where(x=>x.ClientId == id).ToList();
            return data;
        }

        public async Task<InvoiceUpdateForRemoveItemsDto> GetInvoiceByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Invoice/GetInvoiceById/{id}");
            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var content = await Convert<InvoiceDto>(response);
            var updateDto = _mapper.Map<InvoiceDto, InvoiceUpdateForRemoveItemsDto>(content!.Data!, 3);
            return updateDto;
        }

        public async Task<InvoiceCreateDto> CreateInvoiceAsync(InvoiceCreateDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Invoice/CreateInvoice",request);
            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var content = await Convert<InvoiceCreateDto>(response);
            return content!.Data!;
        }

        public async Task UpdateInvoiceAsync(InvoiceUpdateForRemoveItemsDto request)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Invoice/UpdateInvoice", request);
            if (!response.IsSuccessStatusCode)
                throw new Exception();
        }

        public async Task RemoveInvoiceAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Invoice/DeleteInvoice/{id}");
            if (!response.IsSuccessStatusCode)
                throw new Exception();
        }




        public async Task<List<InvoiceItemCreateDto>> AddInvoiceItemsAsync(AddInvoiceItemsDto request)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Invoice/AddInvoiceItems", request);
            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var content = await Convert<List<InvoiceItemCreateDto>>(response);
            return content!.Data!;
        }

        public async Task RemoveInvoiceItemsAsync(RemoveInvoiceItemsDto request)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Invoice/RemoveInvoiceItems", request);
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

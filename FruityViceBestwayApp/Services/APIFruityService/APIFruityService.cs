using FruityViceBestwayApp.Models.Helper;
using FruityViceBestwayApp.Models.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace FruityViceBestwayApp.Services
{
    public class APIFruityService : IAPIFruityService
    {
        private readonly HttpClient _httpClient;
        private readonly FruitViceConfig _fruitViceConfig;
        private readonly IExceptionLoggerService _exceptionLogger;

        public APIFruityService(HttpClient httpClient, IOptions<FruitViceConfig> config, IExceptionLoggerService exceptionLogger)
        {
            _httpClient = httpClient;
            _fruitViceConfig = config.Value;
            _httpClient.BaseAddress = new Uri(_fruitViceConfig.BaseUrl);
            _exceptionLogger = exceptionLogger;
        }

        public async Task<FruitViewModel> GetFruitFromApi(string fruit)
        {
            var response = new FruitViewModel();
            try
            {
                var resp = await _httpClient.GetAsync($"{FruitViceConfig.ApiEndpoints.GetFruit}/{fruit}");
                if (resp.IsSuccessStatusCode)
                {
                    var jsonResponse = await resp.Content.ReadAsStringAsync();
                    response = JsonSerializer.Deserialize<FruitViewModel>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch (Exception ex)
            {
                // Handle other errors
                await _exceptionLogger.LogExceptionAsync(ex);
                throw new Exception("An unexpected error occurred: " + ex.Message);
            }
            return response;
        }
    }
}

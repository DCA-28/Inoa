using StockDecisor.DTO;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace StockDecisor.Service
{
    public class BrapiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BrapiService> _logger;

        public BrapiService(HttpClient httpClient, ILogger<BrapiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<StockResponse?> GetStockInfoAsync(string symbol)
        {
            var apiToken = Environment.GetEnvironmentVariable("BRAPI_TOKEN");
            string url = $"https://brapi.dev/api/quote/{symbol}?token={apiToken}";

            try
            {
                var request = await _httpClient.GetAsync(url);
                var jsonString = await request.Content.ReadAsStringAsync();

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                StockResponse? stockResponse = JsonSerializer.Deserialize<StockResponse>(jsonString, options);
                return stockResponse;
            }
            catch (HttpRequestException httpRequestException)
            {
                _logger.LogError($"Request error for {url}, receiving: {httpRequestException.Message}");
                throw;
            }
            catch (JsonException jsonException)
            {
                _logger.LogError($"Error while deserializing content: {jsonException.Message}");
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError($"General exception: {exception.Message}");
                throw;
            }
        }
    }
}
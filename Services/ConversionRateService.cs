using cp4.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace cp4.Services
{
    public class ConversionRateService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "33f7859e1e50eef068ec4652";

        public ConversionRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IConversionRate> GetConversionRateAsync()
        {
            var response = await _httpClient.GetAsync($"https://v6.exchangerate-api.com/v6/{_apiKey}/latest/USD");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ExchangeRateResponse>();
                var conversionRate = new ConversionRate
                {
                    BRL = result.ConversionRates["BRL"]
                };

                // Conversão explícita para IConversionRate
                return (IConversionRate)conversionRate;
            }

            throw new HttpRequestException("Erro ao obter a taxa de câmbio.");
        }
    }

    public class ExchangeRateResponse
    {
        public Dictionary<string, double> ConversionRates { get; set; }
    }
}

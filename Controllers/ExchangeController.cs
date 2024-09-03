using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace cp4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangeController : ControllerBase, IExchangeController
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "de57eae077d496d8b855b3e3";
        private const string BaseUrl = "https://v6.exchangerate-api.com/v6/";
        private const string CurrencyEndpoint = "latest/USD";

        public ExchangeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ConversionRate), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetExchangeRateAsync()
        {
            try
            {
                string apiUrl = $"{BaseUrl}{ApiKey}/{CurrencyEndpoint}";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(responseData);
                    var brlRate = json["conversion_rates"]?["BRL"]?.Value<double>();

                    if (brlRate.HasValue)
                    {
                        var result = new ConversionRate
                        {
                            BRL = brlRate.Value
                        };

                        return Ok(new
                        {
                            CurrencyPair = "USD/BRL",
                            Rate = result.BRL,
                            Date = DateTime.UtcNow
                        });
                    }

                    return BadRequest("Taxa de câmbio para BRL não encontrada.");
                }

                return StatusCode((int)response.StatusCode, $"Erro na resposta da API: {response.ReasonPhrase}");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Erro na comunicação com a API externa: {ex.Message}");
            }
            catch (JsonException ex)
            {
                return StatusCode(500, $"Erro ao processar os dados JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }

        JsonResult IExchangeController.GetExchangeRate()
        {
            return new JsonResult(GetExchangeRateAsync().Result);
        }
    }
}
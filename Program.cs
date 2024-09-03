using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // URL do endpoint para obter a taxa de câmbio (substitua pela sua chave de API)
        string url = "https://v6.exchangerate-api.com/v6/107e2bc68b3ffd2c375d246f/latest/USD";

        // Cria uma instância do HttpClient
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Faz uma requisição GET para o endpoint da API
                HttpResponseMessage response = await client.GetAsync(url);

                // Verifica se o status da resposta indica sucesso
                if (response.IsSuccessStatusCode)
                {
                    // Lê o conteúdo da resposta como string
                    string responseData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Requisição bem-sucedida!");
                    Console.WriteLine(responseData);
                }
                else
                {
                    Console.WriteLine($"Erro na requisição: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
        }
    }
}

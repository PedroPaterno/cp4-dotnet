using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // URL do endpoint para obter a taxa de c�mbio (substitua pela sua chave de API)
        string url = "https://v6.exchangerate-api.com/v6/107e2bc68b3ffd2c375d246f/latest/USD";

        // Cria uma inst�ncia do HttpClient
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Faz uma requisi��o GET para o endpoint da API
                HttpResponseMessage response = await client.GetAsync(url);

                // Verifica se o status da resposta indica sucesso
                if (response.IsSuccessStatusCode)
                {
                    // L� o conte�do da resposta como string
                    string responseData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Requisi��o bem-sucedida!");
                    Console.WriteLine(responseData);
                }
                else
                {
                    Console.WriteLine($"Erro na requisi��o: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
        }
    }
}

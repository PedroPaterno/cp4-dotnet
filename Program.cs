using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        
        string url = "https://v6.exchangerate-api.com/v6/33f7859e1e50eef068ec4652/latest/USD";

        
        using (HttpClient client = new HttpClient())
        {
            try
            {
                
                HttpResponseMessage response = await client.GetAsync(url);

                
                if (response.IsSuccessStatusCode)
                {
                    
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

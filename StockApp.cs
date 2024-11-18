using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.Json;
using StockDTO;

class StockApp
{
    
    public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (var httpClient = new HttpClient())
            {
                double sellPrice = Convert.ToDouble(args[0]);
                double buyPrice =  Convert.ToDouble(args[1]);
                String stockSymbol = args[2];

                var url = $"https://brapi.dev/api/quote/PETR4?token=drWj61XX5ZomtHNqre9qB7";
                var request = await httpClient.GetAsync(url);
                var jsonString = await request.Content.ReadAsStringAsync();

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                StockResponse? stockResponse = JsonSerializer.Deserialize<StockResponse>(jsonString, options);
                StockInfo? stockInfo = stockResponse?.Results?.Where(s => s.Symbol == "PETR4").FirstOrDefault();
                var marketPrice = stockInfo?.RegularMarketPrice;

                if (marketPrice >= sellPrice)
                {
                    Console.WriteLine($"Selling Stock {stockSymbol}");    
                }

                else
                {
                    Console.WriteLine($"Buying Stock {stockSymbol}");
                }

                Console.WriteLine($"Final execution");


            }
        }
}
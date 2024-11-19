using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using StockDecisor.Service;
using StockDTO;

class StockApp
{
    
    public static async Task Main(string[] args)
        {
            using (var httpClient = new HttpClient())
            {
                double sellPrice = Convert.ToDouble(args[0]);
                double buyPrice =  Convert.ToDouble(args[1]);
                String stockSymbol = args[2];

                using var loggerFactory = LoggerFactory.Create(loggingBuilder =>
                    loggingBuilder.SetMinimumLevel(LogLevel.Trace));

                var logger = loggerFactory.CreateLogger<BrapiService>();

                BrapiService brapiService = new BrapiService(httpClient, logger);

                StockResponse? stockResponse = await brapiService.GetStockInfoAsync(stockSymbol);
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
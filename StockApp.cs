using Microsoft.Extensions.Logging;
using StockDecisor.Service;
using StockDecisor.DTO;
using StockDecisor.BLL;

class StockApp
{
    public static async Task Main(string[] args)
        {
            using var httpClient = new HttpClient();
            using var loggerFactory = LoggerFactory.Create(builder =>
                builder.SetMinimumLevel(LogLevel.Trace));

            var logger = loggerFactory.CreateLogger<BrapiService>();

            BrapiService brapiService = new BrapiService(httpClient, logger);
            StockParameter stockParameter = new StockParameter
            {
                SellPrice =  Convert.ToDouble(args[0]),
                BuyPrice = Convert.ToDouble(args[1]),
                Symbol = args[2]
            };

            StockResponse? stockResponse = await brapiService.GetStockInfoAsync(stockParameter.Symbol);
            StockTrading.BuyOrSellStock(stockResponse, stockParameter);
    }
}
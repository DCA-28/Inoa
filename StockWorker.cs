using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using StockDecisor.Service;
using StockDecisor.DTO;
using StockDecisor.BLL;


namespace StockDecisor.Worker
{
    public class StockWorker : BackgroundService
    {
        private readonly ILogger<StockWorker> _logger;
        private readonly BrapiService _brapiService;

        public StockWorker(ILogger<StockWorker> logger, BrapiService brapiService)
        {
            _logger = logger;
            _brapiService = brapiService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var waitingTime = Environment.GetEnvironmentVariable("WAITING_TIME_MILLISECONDS");

            while (!stoppingToken.IsCancellationRequested)
            {
                var args = Environment.GetCommandLineArgs();

                _logger.LogInformation($"Starting StockWorker to decide about {args[3]} at {DateTimeOffset.Now}");

                StockParameter stockParameter = new StockParameter
                {
                    SellPrice =  Convert.ToDouble(args[1]),
                    BuyPrice = Convert.ToDouble(args[2]),
                    Symbol = args[3]
                };

                StockResponse? stockResponse = await _brapiService.GetStockInfoAsync(stockParameter.Symbol);
                StockTrading.BuyOrSellStock(stockResponse, stockParameter);

                _logger.LogInformation($"Ending StockWorker to decide about {args[3]} at {DateTimeOffset.Now}");

                await Task.Delay(Convert.ToInt32(waitingTime), stoppingToken);
            }
        }
    }
}
using StockDecisor.DTO;

namespace StockDecisor.BLL
{
    public static class StockTrading
    {
        public static void BuyOrSellStock(StockResponse stockResponse, StockParameter stockParameter)
        {
            StockInfo? stockInfo = stockResponse?.Results?.Where(s => s.Symbol == stockParameter.Symbol).FirstOrDefault();
            var marketPrice = stockInfo?.RegularMarketPrice; 

            if (marketPrice >= stockParameter.SellPrice)
            {
                Console.WriteLine($"Selling {stockParameter.Symbol}");
            }
            else if (marketPrice <= stockParameter.BuyPrice)
            {
                Console.WriteLine($"Buying {stockParameter.Symbol}");
            }
            else
            {
                Console.WriteLine($"{stockParameter.Symbol} is to cheap to sell but expensive to buy, keep it in the wallet!");
            }
        }
    }
}
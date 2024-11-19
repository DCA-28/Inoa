namespace StockDecisor.DTO
{
    public class StockResponse
    {
        public List<StockInfo>? Results {get; set;}
        public string? RequestedAt {get; set;}
        public string? Took {get; set;}
    }
}
namespace Api.Models
{
    public class CompanyStockExchange
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public int StockExchangeId { get; set; }
        public StockExchange StockExchange { get; set; }
    }
}

using System.Collections.Generic;

namespace Api.Models
{
    public class StockExchange
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brief { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public List<CompanyStockExchange> CompanyStockExchanges { get; set; }
        public List<StockPrice> StockPrices { get; set; }
        public List<IPODetail> IPODetails { get; set; }
    }
}

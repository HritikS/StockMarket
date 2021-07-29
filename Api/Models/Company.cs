using System.Collections.Generic;

namespace Api.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Turnover { get; set; }
        public string CEO { get; set; }
        public string BOD { get; set; }
        public List<CompanyStockExchange> CompanyStockExchanges { get; set; }
        public List<StockPrice> StockPrices { get; set; }
        public List<IPODetail> IPODetails { get; set; }
        public int SectorId { get; set; }
        public Sector Sector { get; set; }
        public string Brief { get; set; }
    }
}

using System;

namespace Api.Models
{
    public class StockPrice
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public int StockExchangeId { get; set; }
        public StockExchange StockExchange { get; set; }

        public int CurrentPrice { get; set; }
        public DateTime DateTime { get; set; }
    }
}

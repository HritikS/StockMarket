using System;

namespace Api.Models
{
    public class IPODetail
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public int StockExchangeId { get; set; }
        public StockExchange StockExchange { get; set; }

        public int PPS { get; set; }
        public int TNOS { get; set; }
        public DateTime OpenDateTime { get; set; }
        public string Remarks { get; set; }
    }
}

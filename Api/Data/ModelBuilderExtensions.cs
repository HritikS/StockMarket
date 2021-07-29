using Api.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sector>().HasData(
                new Sector { Id = 1, Name = "Sector1", Brief = "1" },
                new Sector { Id = 2, Name = "Sector2", Brief = "2" },
                new Sector { Id = 3, Name = "Sector3", Brief = "3" }
            );

            modelBuilder.Entity<StockExchange>().HasData(
                new StockExchange { Id = 1, Name = "SE1", Brief = "1", Address = "A1", Remarks = "R1" },
                new StockExchange { Id = 2, Name = "SE2", Brief = "2", Address = "A2", Remarks = "R2" },
                new StockExchange { Id = 3, Name = "SE3", Brief = "3", Address = "A3", Remarks = "R3" }
            );

            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "Company1", Turnover = 1, CEO = "CEO1", BOD = "BOD1", SectorId = 1, Brief = "1" },
                new Company { Id = 2, Name = "Company2", Turnover = 2, CEO = "CEO2", BOD = "BOD2", SectorId = 2, Brief = "2" },
                new Company { Id = 3, Name = "Company3", Turnover = 3, CEO = "CEO3", BOD = "BOD3", SectorId = 3, Brief = "3" }
            );

            modelBuilder.Entity<CompanyStockExchange>().HasData(
                new CompanyStockExchange { CompanyId = 1, StockExchangeId = 1 },
                new CompanyStockExchange { CompanyId = 2, StockExchangeId = 2 },
                new CompanyStockExchange { CompanyId = 3, StockExchangeId = 3 }
            );

            modelBuilder.Entity<IPODetail>().HasData(
                new IPODetail { CompanyId = 1, StockExchangeId = 1, PPS = 1, TNOS = 1, OpenDateTime = DateTime.Now, Remarks = "R1" },
                new IPODetail { CompanyId = 2, StockExchangeId = 2, PPS = 2, TNOS = 2, OpenDateTime = DateTime.Now, Remarks = "R2" },
                new IPODetail { CompanyId = 3, StockExchangeId = 3, PPS = 3, TNOS = 3, OpenDateTime = DateTime.Now, Remarks = "R3" }
            );

            modelBuilder.Entity<StockPrice>().HasData(
                new StockPrice { CompanyId = 1, StockExchangeId = 1, DateTime = new DateTime(2021, 07, 16, 15, 0, 0), CurrentPrice = 1 },
                new StockPrice { CompanyId = 2, StockExchangeId = 2, DateTime = new DateTime(2021, 07, 16, 16, 0, 0), CurrentPrice = 2 },
                new StockPrice { CompanyId = 3, StockExchangeId = 3, DateTime = new DateTime(2021, 07, 16, 17, 0, 0), CurrentPrice = 3 }
            );
        }
    }
}

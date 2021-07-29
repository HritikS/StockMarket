using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Sector> Sectors { get; set; }
        public DbSet<StockExchange> StockExchanges { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyStockExchange> CompanyStockExchanges { get; set; }
        public DbSet<IPODetail> IPODetails { get; set; }
        public DbSet<StockPrice> StockPrices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyStockExchange>().HasKey(cse => new { cse.CompanyId, cse.StockExchangeId });
            modelBuilder.Entity<CompanyStockExchange>().HasOne(cse => cse.StockExchange).WithMany(se => se.CompanyStockExchanges).HasForeignKey(cse => cse.StockExchangeId);
            modelBuilder.Entity<CompanyStockExchange>().HasOne(cse => cse.Company).WithMany(c => c.CompanyStockExchanges).HasForeignKey(cse => cse.CompanyId);

            modelBuilder.Entity<IPODetail>().HasKey(id => new { id.CompanyId, id.StockExchangeId });
            modelBuilder.Entity<IPODetail>().HasOne(id => id.StockExchange).WithMany(se => se.IPODetails).HasForeignKey(id => id.StockExchangeId);
            modelBuilder.Entity<IPODetail>().HasOne(id => id.Company).WithMany(c => c.IPODetails).HasForeignKey(id => id.CompanyId);

            modelBuilder.Entity<StockPrice>().HasKey(sp => new { sp.CompanyId, sp.StockExchangeId, sp.DateTime });
            modelBuilder.Entity<StockPrice>().HasOne(sp => sp.StockExchange).WithMany(se => se.StockPrices).HasForeignKey(sp => sp.StockExchangeId);
            modelBuilder.Entity<StockPrice>().HasOne(sp => sp.Company).WithMany(c => c.StockPrices).HasForeignKey(sp => sp.CompanyId);

            modelBuilder.Seed();
        }
    }
}

// <auto-generated />
using System;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210719035856_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Api.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BOD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Brief")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CEO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SectorId")
                        .HasColumnType("int");

                    b.Property<int>("Turnover")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SectorId");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BOD = "BOD1",
                            Brief = "1",
                            CEO = "CEO1",
                            Name = "Company1",
                            SectorId = 1,
                            Turnover = 1
                        },
                        new
                        {
                            Id = 2,
                            BOD = "BOD2",
                            Brief = "2",
                            CEO = "CEO2",
                            Name = "Company2",
                            SectorId = 2,
                            Turnover = 2
                        },
                        new
                        {
                            Id = 3,
                            BOD = "BOD3",
                            Brief = "3",
                            CEO = "CEO3",
                            Name = "Company3",
                            SectorId = 3,
                            Turnover = 3
                        });
                });

            modelBuilder.Entity("Api.Models.CompanyStockExchange", b =>
                {
                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("StockExchangeId")
                        .HasColumnType("int");

                    b.HasKey("CompanyId", "StockExchangeId");

                    b.HasIndex("StockExchangeId");

                    b.ToTable("CompanyStockExchanges");

                    b.HasData(
                        new
                        {
                            CompanyId = 1,
                            StockExchangeId = 1
                        },
                        new
                        {
                            CompanyId = 2,
                            StockExchangeId = 2
                        },
                        new
                        {
                            CompanyId = 3,
                            StockExchangeId = 3
                        });
                });

            modelBuilder.Entity("Api.Models.IPODetail", b =>
                {
                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("StockExchangeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OpenDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("PPS")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TNOS")
                        .HasColumnType("int");

                    b.HasKey("CompanyId", "StockExchangeId");

                    b.HasIndex("StockExchangeId");

                    b.ToTable("IPODetails");

                    b.HasData(
                        new
                        {
                            CompanyId = 1,
                            StockExchangeId = 1,
                            OpenDateTime = new DateTime(2021, 7, 19, 9, 28, 55, 995, DateTimeKind.Local).AddTicks(8636),
                            PPS = 1,
                            Remarks = "R1",
                            TNOS = 1
                        },
                        new
                        {
                            CompanyId = 2,
                            StockExchangeId = 2,
                            OpenDateTime = new DateTime(2021, 7, 19, 9, 28, 55, 997, DateTimeKind.Local).AddTicks(2123),
                            PPS = 2,
                            Remarks = "R2",
                            TNOS = 2
                        },
                        new
                        {
                            CompanyId = 3,
                            StockExchangeId = 3,
                            OpenDateTime = new DateTime(2021, 7, 19, 9, 28, 55, 997, DateTimeKind.Local).AddTicks(2253),
                            PPS = 3,
                            Remarks = "R3",
                            TNOS = 3
                        });
                });

            modelBuilder.Entity("Api.Models.Sector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brief")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sectors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brief = "1",
                            Name = "Sector1"
                        },
                        new
                        {
                            Id = 2,
                            Brief = "2",
                            Name = "Sector2"
                        },
                        new
                        {
                            Id = 3,
                            Brief = "3",
                            Name = "Sector3"
                        });
                });

            modelBuilder.Entity("Api.Models.StockExchange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Brief")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StockExchanges");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "A1",
                            Brief = "1",
                            Name = "SE1",
                            Remarks = "R1"
                        },
                        new
                        {
                            Id = 2,
                            Address = "A2",
                            Brief = "2",
                            Name = "SE2",
                            Remarks = "R2"
                        },
                        new
                        {
                            Id = 3,
                            Address = "A3",
                            Brief = "3",
                            Name = "SE3",
                            Remarks = "R3"
                        });
                });

            modelBuilder.Entity("Api.Models.StockPrice", b =>
                {
                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("StockExchangeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentPrice")
                        .HasColumnType("int");

                    b.HasKey("CompanyId", "StockExchangeId", "DateTime");

                    b.HasIndex("StockExchangeId");

                    b.ToTable("StockPrices");

                    b.HasData(
                        new
                        {
                            CompanyId = 1,
                            StockExchangeId = 1,
                            DateTime = new DateTime(2021, 7, 16, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 1
                        },
                        new
                        {
                            CompanyId = 2,
                            StockExchangeId = 2,
                            DateTime = new DateTime(2021, 7, 16, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 2
                        },
                        new
                        {
                            CompanyId = 3,
                            StockExchangeId = 3,
                            DateTime = new DateTime(2021, 7, 16, 17, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 3
                        });
                });

            modelBuilder.Entity("Api.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Confirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("UserType")
                        .HasColumnType("bit");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Confirmed = true,
                            Email = "user1@gmail.com",
                            MobileNumber = "1111111111",
                            Password = "user1",
                            UserType = true,
                            Username = "user1"
                        },
                        new
                        {
                            Id = 2,
                            Confirmed = true,
                            Email = "user2@gmail.com",
                            MobileNumber = "2222222222",
                            Password = "user2",
                            UserType = false,
                            Username = "user2"
                        },
                        new
                        {
                            Id = 3,
                            Confirmed = true,
                            Email = "user3@gmail.com",
                            MobileNumber = "3333333333",
                            Password = "user3",
                            UserType = false,
                            Username = "user3"
                        },
                        new
                        {
                            Id = 4,
                            Confirmed = false,
                            Email = "user4@gmail.com",
                            MobileNumber = "4444444444",
                            Password = "user4",
                            UserType = false,
                            Username = "user4"
                        });
                });

            modelBuilder.Entity("Api.Models.Company", b =>
                {
                    b.HasOne("Api.Models.Sector", "Sector")
                        .WithMany("Companies")
                        .HasForeignKey("SectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Models.CompanyStockExchange", b =>
                {
                    b.HasOne("Api.Models.Company", "Company")
                        .WithMany("CompanyStockExchanges")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.StockExchange", "StockExchange")
                        .WithMany("CompanyStockExchanges")
                        .HasForeignKey("StockExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Models.IPODetail", b =>
                {
                    b.HasOne("Api.Models.Company", "Company")
                        .WithMany("IPODetails")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.StockExchange", "StockExchange")
                        .WithMany("IPODetails")
                        .HasForeignKey("StockExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Models.StockPrice", b =>
                {
                    b.HasOne("Api.Models.Company", "Company")
                        .WithMany("StockPrices")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.StockExchange", "StockExchange")
                        .WithMany("StockPrices")
                        .HasForeignKey("StockExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

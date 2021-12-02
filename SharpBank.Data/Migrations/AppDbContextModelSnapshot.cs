﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharpBank.Data;

#nullable disable

namespace SharpBank.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SharpBank.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("BankId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("45a6f085-ff0e-491b-9687-6437832023b4"),
                            Balance = 20m,
                            BankId = new Guid("88cc47df-ab87-4340-9f47-787a05b72ac2"),
                            Gender = 2,
                            Name = "Babu",
                            Password = "1234",
                            Status = 1,
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("553928da-fa6c-4415-82e9-f07158c34241"),
                            Balance = 201m,
                            BankId = new Guid("88cc47df-ab87-4340-9f47-787a05b72ac2"),
                            Gender = 2,
                            Name = "Baba",
                            Password = "1234",
                            Status = 1,
                            Type = 2
                        });
                });

            modelBuilder.Entity("SharpBank.Models.Bank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DefaultCurrencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("IMPSToOther")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("IMPSToSame")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("RTGSToOther")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RTGSToSame")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DefaultCurrencyId");

                    b.ToTable("Banks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("88cc47df-ab87-4340-9f47-787a05b72ac2"),
                            CreatedBy = "God",
                            CreatedOn = new DateTime(2021, 12, 2, 11, 34, 12, 302, DateTimeKind.Local).AddTicks(3206),
                            DefaultCurrencyId = new Guid("a943f9bc-86dc-424d-b636-d3c80ee4273f"),
                            IMPSToOther = 0.07m,
                            IMPSToSame = 0.03m,
                            Name = "Kotha Bank",
                            RTGSToOther = 0.05m,
                            RTGSToSame = 0.0m,
                            UpdatedBy = "God",
                            UpdatedOn = new DateTime(2021, 12, 2, 11, 34, 12, 302, DateTimeKind.Local).AddTicks(3218)
                        });
                });

            modelBuilder.Entity("SharpBank.Models.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BankId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ExchangeRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a943f9bc-86dc-424d-b636-d3c80ee4273f"),
                            Code = "INR",
                            ExchangeRate = 1m,
                            Name = "Desi Rupee"
                        });
                });

            modelBuilder.Entity("SharpBank.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("DestinationAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Mode")
                        .HasColumnType("int");

                    b.Property<decimal>("NetAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("On")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SourceAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TransactionCharges")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DestinationAccountId");

                    b.HasIndex("SourceAccountId");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fd5a9a18-9f78-4d2c-a357-337e062c2bb5"),
                            Amount = 10m,
                            DestinationAccountId = new Guid("553928da-fa6c-4415-82e9-f07158c34241"),
                            Mode = 0,
                            NetAmount = 10.1m,
                            On = new DateTime(2021, 12, 2, 11, 34, 12, 302, DateTimeKind.Local).AddTicks(3255),
                            SourceAccountId = new Guid("45a6f085-ff0e-491b-9687-6437832023b4"),
                            TransactionCharges = 0.1m,
                            Type = 0
                        });
                });

            modelBuilder.Entity("SharpBank.Models.Account", b =>
                {
                    b.HasOne("SharpBank.Models.Bank", "Bank")
                        .WithMany("Accounts")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("SharpBank.Models.Bank", b =>
                {
                    b.HasOne("SharpBank.Models.Currency", "DefaultCurrency")
                        .WithMany()
                        .HasForeignKey("DefaultCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DefaultCurrency");
                });

            modelBuilder.Entity("SharpBank.Models.Currency", b =>
                {
                    b.HasOne("SharpBank.Models.Bank", null)
                        .WithMany("Currencies")
                        .HasForeignKey("BankId");
                });

            modelBuilder.Entity("SharpBank.Models.Transaction", b =>
                {
                    b.HasOne("SharpBank.Models.Account", "DestinationAccount")
                        .WithMany("CreditTransactions")
                        .HasForeignKey("DestinationAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SharpBank.Models.Account", "SourceAccount")
                        .WithMany("DebitTransactions")
                        .HasForeignKey("SourceAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DestinationAccount");

                    b.Navigation("SourceAccount");
                });

            modelBuilder.Entity("SharpBank.Models.Account", b =>
                {
                    b.Navigation("CreditTransactions");

                    b.Navigation("DebitTransactions");
                });

            modelBuilder.Entity("SharpBank.Models.Bank", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Currencies");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharpBank.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Mode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_DestinationAccountId",
                        column: x => x.DestinationAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_SourceAccountId",
                        column: x => x.SourceAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultCurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RTGSToSame = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RTGSToOther = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IMPSToSame = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IMPSToOther = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currencies_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "BankId", "Code", "ExchangeRate", "Name" },
                values: new object[] { new Guid("a943f9bc-86dc-424d-b636-d3c80ee4273f"), null, "INR", 1m, "Desi Rupee" });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DefaultCurrencyId", "IMPSToOther", "IMPSToSame", "Name", "RTGSToOther", "RTGSToSame", "UpdatedBy", "UpdatedOn" },
                values: new object[] { new Guid("88cc47df-ab87-4340-9f47-787a05b72ac2"), "God", new DateTime(2021, 12, 2, 11, 34, 12, 302, DateTimeKind.Local).AddTicks(3206), new Guid("a943f9bc-86dc-424d-b636-d3c80ee4273f"), 0.07m, 0.03m, "Kotha Bank", 0.05m, 0.0m, "God", new DateTime(2021, 12, 2, 11, 34, 12, 302, DateTimeKind.Local).AddTicks(3218) });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Balance", "BankId", "Gender", "Name", "Password", "Status", "Type" },
                values: new object[] { new Guid("45a6f085-ff0e-491b-9687-6437832023b4"), 20m, new Guid("88cc47df-ab87-4340-9f47-787a05b72ac2"), 2, "Babu", "1234", 1, 2 });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Balance", "BankId", "Gender", "Name", "Password", "Status", "Type" },
                values: new object[] { new Guid("553928da-fa6c-4415-82e9-f07158c34241"), 201m, new Guid("88cc47df-ab87-4340-9f47-787a05b72ac2"), 2, "Baba", "1234", 1, 2 });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "DestinationAccountId", "Mode", "NetAmount", "On", "SourceAccountId", "TransactionCharges", "Type" },
                values: new object[] { new Guid("fd5a9a18-9f78-4d2c-a357-337e062c2bb5"), 10m, new Guid("553928da-fa6c-4415-82e9-f07158c34241"), 0, 10.1m, new DateTime(2021, 12, 2, 11, 34, 12, 302, DateTimeKind.Local).AddTicks(3255), new Guid("45a6f085-ff0e-491b-9687-6437832023b4"), 0.1m, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BankId",
                table: "Accounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_DefaultCurrencyId",
                table: "Banks",
                column: "DefaultCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_BankId",
                table: "Currencies",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DestinationAccountId",
                table: "Transactions",
                column: "DestinationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SourceAccountId",
                table: "Transactions",
                column: "SourceAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Banks_BankId",
                table: "Accounts",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Currencies_DefaultCurrencyId",
                table: "Banks",
                column: "DefaultCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Banks_BankId",
                table: "Currencies");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}

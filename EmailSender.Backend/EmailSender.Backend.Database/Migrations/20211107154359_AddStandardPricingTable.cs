﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailSender.Backend.Database.Migrations
{
    public partial class AddStandardPricingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StandardPricing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerApiRequest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PerSentEmail = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyIso = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Terms = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardPricing", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StandardPricing");
        }
    }
}
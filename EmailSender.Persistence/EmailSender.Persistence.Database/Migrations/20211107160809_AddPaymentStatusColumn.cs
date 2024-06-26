﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailSender.Persistence.Database.Migrations
{
    public partial class AddPaymentStatusColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Billing",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Billing");
        }
    }
}

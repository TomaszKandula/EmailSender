﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailSender.Persistence.Database.Migrations
{
    public partial class RenameColumnNameRequestHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Requested",
                table: "RequestsHistory",
                newName: "RequestedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestedAt",
                table: "RequestsHistory",
                newName: "Requested");
        }
    }
}

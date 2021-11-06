﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailSender.Backend.Database.Migrations
{
    public partial class RenameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EndpointName",
                table: "RequestHistory",
                newName: "RequestName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestName",
                table: "RequestHistory",
                newName: "EndpointName");
        }
    }
}

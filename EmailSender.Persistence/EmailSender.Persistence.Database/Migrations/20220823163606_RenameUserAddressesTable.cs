﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailSender.Persistence.Database.Migrations
{
    public partial class RenameUserAddressesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserIpAddresses");

            migrationBuilder.CreateTable(
                name: "UserAllowedIps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAllowedIps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAllowedIps_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAllowedIps_UserId",
                table: "UserAllowedIps",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAllowedIps");

            migrationBuilder.CreateTable(
                name: "UserIpAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserIpAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserIpAddresses_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserIpAddresses_UserId",
                table: "UserIpAddresses",
                column: "UserId");
        }
    }
}

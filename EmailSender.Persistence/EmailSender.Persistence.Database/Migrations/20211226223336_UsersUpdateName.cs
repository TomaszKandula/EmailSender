﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailSender.Persistence.Database.Migrations
{
    public partial class UsersUpdateName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllowDomain_User",
                table: "AllowDomain");

            migrationBuilder.DropForeignKey(
                name: "FK_AllowEmail_User",
                table: "AllowEmail");

            migrationBuilder.DropForeignKey(
                name: "FK_Billing_User",
                table: "Billing");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailHistory_User",
                table: "EmailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Pricing_User",
                table: "Pricing");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestHistory_User",
                table: "RequestHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_User_UserId",
                table: "UserDetails");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserAlias = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrivateKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AllowDomain_Users",
                table: "AllowDomain",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AllowEmail_Users",
                table: "AllowEmail",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Billing_Users",
                table: "Billing",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailHistory_Users",
                table: "EmailHistory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pricing_Users",
                table: "Pricing",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestHistory_Users",
                table: "RequestHistory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_Users",
                table: "UserDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllowDomain_Users",
                table: "AllowDomain");

            migrationBuilder.DropForeignKey(
                name: "FK_AllowEmail_Users",
                table: "AllowEmail");

            migrationBuilder.DropForeignKey(
                name: "FK_Billing_Users",
                table: "Billing");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailHistory_Users",
                table: "EmailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Pricing_Users",
                table: "Pricing");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestHistory_Users",
                table: "RequestHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_Users",
                table: "UserDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrivateKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserAlias = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AllowDomain_User",
                table: "AllowDomain",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AllowEmail_User",
                table: "AllowEmail",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Billing_User",
                table: "Billing",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailHistory_User",
                table: "EmailHistory",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pricing_User",
                table: "Pricing",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestHistory_User",
                table: "RequestHistory",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_User_UserId",
                table: "UserDetails",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

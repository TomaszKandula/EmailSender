﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailSender.Backend.Database.Migrations
{
    public partial class RenameEmailsHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailsHistory");

            migrationBuilder.CreateTable(
                name: "SentHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sent = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SentHistory_Emails",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SentHistory_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SentHistory_EmailId",
                table: "SentHistory",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_SentHistory_UserId",
                table: "SentHistory",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SentHistory");

            migrationBuilder.CreateTable(
                name: "EmailsHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailsHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailsHistory_Emails",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailsHistory_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailsHistory_EmailId",
                table: "EmailsHistory",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailsHistory_UserId",
                table: "EmailsHistory",
                column: "UserId");
        }
    }
}

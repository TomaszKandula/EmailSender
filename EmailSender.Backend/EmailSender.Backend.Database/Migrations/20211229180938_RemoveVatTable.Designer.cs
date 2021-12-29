﻿// <auto-generated />
using System;
using EmailSender.Backend.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmailSender.Backend.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20211229180938_RemoveVatTable")]
    partial class RemoveVatTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.Billings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CurrencyIso")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("InvoiceSentDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("IssuedInvoice")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ValueDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Billings");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.Emails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ServerKey")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ServerName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("ServerPort")
                        .HasColumnType("int");

                    b.Property<bool>("ServerSsl")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.EmailsHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Sent")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EmailId");

                    b.HasIndex("UserId");

                    b.ToTable("EmailsHistory");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.Pricing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CurrencyIso")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("DiscountMaturity")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PerApiRequest")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PerSentEmail")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Terms")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Pricing");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.RequestsHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RequestName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Requested")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RequestsHistory");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.StandardPricing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CurrencyIso")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<decimal>("PerApiRequest")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PerSentEmail")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Terms")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("StandardPricing");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.UserDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VatNumber")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.UserDomains", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserDomains");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.UserEmails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EmailId");

                    b.HasIndex("UserId");

                    b.ToTable("UserEmails");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PrivateKey")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Registered")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserAlias")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.Billings", b =>
                {
                    b.HasOne("EmailSender.Backend.Domain.Entities.Users", "Users")
                        .WithMany("Billings")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Billings_Users")
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.EmailsHistory", b =>
                {
                    b.HasOne("EmailSender.Backend.Domain.Entities.Emails", "Emails")
                        .WithMany("EmailsHistory")
                        .HasForeignKey("EmailId")
                        .HasConstraintName("FK_EmailsHistory_Emails")
                        .IsRequired();

                    b.HasOne("EmailSender.Backend.Domain.Entities.Users", "Users")
                        .WithMany("EmailsHistory")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_EmailsHistory_Users")
                        .IsRequired();

                    b.Navigation("Emails");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.Pricing", b =>
                {
                    b.HasOne("EmailSender.Backend.Domain.Entities.Users", "Users")
                        .WithMany("Pricing")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Pricing_Users")
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.RequestsHistory", b =>
                {
                    b.HasOne("EmailSender.Backend.Domain.Entities.Users", "Users")
                        .WithMany("RequestsHistory")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_RequestsHistory_Users")
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.UserDetails", b =>
                {
                    b.HasOne("EmailSender.Backend.Domain.Entities.Users", "Users")
                        .WithMany("UserDetails")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserDetails_Users")
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.UserDomains", b =>
                {
                    b.HasOne("EmailSender.Backend.Domain.Entities.Users", "Users")
                        .WithMany("UserDomains")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserDomains_Users")
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.UserEmails", b =>
                {
                    b.HasOne("EmailSender.Backend.Domain.Entities.Emails", "Emails")
                        .WithMany("UserEmails")
                        .HasForeignKey("EmailId")
                        .HasConstraintName("FK_UserEmails_Emails")
                        .IsRequired();

                    b.HasOne("EmailSender.Backend.Domain.Entities.Users", "Users")
                        .WithMany("UserEmails")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserEmails_Users")
                        .IsRequired();

                    b.Navigation("Emails");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.Emails", b =>
                {
                    b.Navigation("EmailsHistory");

                    b.Navigation("UserEmails");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.Users", b =>
                {
                    b.Navigation("Billings");

                    b.Navigation("EmailsHistory");

                    b.Navigation("Pricing");

                    b.Navigation("RequestsHistory");

                    b.Navigation("UserDetails");

                    b.Navigation("UserDomains");

                    b.Navigation("UserEmails");
                });
#pragma warning restore 612, 618
        }
    }
}

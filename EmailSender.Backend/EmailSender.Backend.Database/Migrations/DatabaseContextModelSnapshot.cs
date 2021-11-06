﻿// <auto-generated />
using System;
using EmailSender.Backend.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmailSender.Backend.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.AllowDomain", b =>
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

                    b.ToTable("AllowDomain");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.AllowEmail", b =>
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

                    b.ToTable("AllowEmail");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.Billing", b =>
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

                    b.Property<bool>("IsInvoiceSent")
                        .HasColumnType("bit");

                    b.Property<byte[]>("IssuedInvoice")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ValueDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Billing");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.Email", b =>
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

                    b.ToTable("Email");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.EmailHistory", b =>
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

                    b.ToTable("EmailHistory");
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

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.RequestHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EndpointName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Requested")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RequestHistory");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.User", b =>
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

                    b.ToTable("User");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.AllowDomain", b =>
                {
                    b.HasOne("EmailSender.Backend.Domain.Entities.User", "User")
                        .WithMany("AllowDomain")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_AllowDomain_User")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.AllowEmail", b =>
                {
                    b.HasOne("EmailSender.Backend.Domain.Entities.Email", "Email")
                        .WithMany("AllowEmail")
                        .HasForeignKey("EmailId")
                        .HasConstraintName("FK_AllowEmail_Email")
                        .IsRequired();

                    b.HasOne("EmailSender.Backend.Domain.Entities.User", "User")
                        .WithMany("AllowEmail")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_AllowEmail_User")
                        .IsRequired();

                    b.Navigation("Email");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.Billing", b =>
                {
                    b.HasOne("EmailSender.Backend.Domain.Entities.User", "User")
                        .WithMany("Billing")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Billing_User")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.EmailHistory", b =>
                {
                    b.HasOne("EmailSender.Backend.Domain.Entities.Email", "Email")
                        .WithMany("EmailHistory")
                        .HasForeignKey("EmailId")
                        .HasConstraintName("FK_EmailHistory_Email")
                        .IsRequired();

                    b.HasOne("EmailSender.Backend.Domain.Entities.User", "User")
                        .WithMany("EmailHistory")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_EmailHistory_User")
                        .IsRequired();

                    b.Navigation("Email");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.Pricing", b =>
                {
                    b.HasOne("EmailSender.Backend.Domain.Entities.User", "User")
                        .WithMany("Pricing")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Pricing_User")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.RequestHistory", b =>
                {
                    b.HasOne("EmailSender.Backend.Domain.Entities.User", "User")
                        .WithMany("RequestHistory")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_RequestHistory_User")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.Email", b =>
                {
                    b.Navigation("AllowEmail");

                    b.Navigation("EmailHistory");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.User", b =>
                {
                    b.Navigation("AllowDomain");

                    b.Navigation("AllowEmail");

                    b.Navigation("Billing");

                    b.Navigation("EmailHistory");

                    b.Navigation("Pricing");

                    b.Navigation("RequestHistory");
                });
#pragma warning restore 612, 618
        }
    }
}

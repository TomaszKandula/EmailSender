﻿// <auto-generated />
using System;
using EmailSender.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmailSender.Persistence.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220823163606_RenameUserAddressesTable")]
    partial class RenameUserAddressesTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.UserAllowedIps", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserAllowedIps");
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

                    b.Property<bool>("IsDeleted")
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

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.RequestsHistory", b =>
                {
                    b.HasOne("EmailSender.Backend.Domain.Entities.Users", "Users")
                        .WithMany("RequestsHistory")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_RequestsHistory_Users")
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("EmailSender.Backend.Domain.Entities.UserAllowedIps", b =>
                {
                    b.HasOne("EmailSender.Backend.Domain.Entities.Users", "Users")
                        .WithMany("UserAllowedIps")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserAllowedIps_Users")
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
                    b.Navigation("EmailsHistory");

                    b.Navigation("RequestsHistory");

                    b.Navigation("UserAllowedIps");

                    b.Navigation("UserDetails");

                    b.Navigation("UserEmails");
                });
#pragma warning restore 612, 618
        }
    }
}
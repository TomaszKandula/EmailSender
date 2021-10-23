﻿// <auto-generated />
using System;
using EmailSenderService.Backend.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmailSenderService.Backend.Database.Migrations
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

            modelBuilder.Entity("EmailSenderService.Backend.Domain.Entities.Email", b =>
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

                    b.HasKey("Id");

                    b.ToTable("Email");
                });

            modelBuilder.Entity("EmailSenderService.Backend.Domain.Entities.History", b =>
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

                    b.ToTable("History");
                });

            modelBuilder.Entity("EmailSenderService.Backend.Domain.Entities.RegisteredEmail", b =>
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

                    b.ToTable("RegisteredEmail");
                });

            modelBuilder.Entity("EmailSenderService.Backend.Domain.Entities.User", b =>
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

            modelBuilder.Entity("EmailSenderService.Backend.Domain.Entities.History", b =>
                {
                    b.HasOne("EmailSenderService.Backend.Domain.Entities.Email", "Email")
                        .WithMany("History")
                        .HasForeignKey("EmailId")
                        .HasConstraintName("FK_History_Email")
                        .IsRequired();

                    b.HasOne("EmailSenderService.Backend.Domain.Entities.User", "User")
                        .WithMany("History")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_History_User")
                        .IsRequired();

                    b.Navigation("Email");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmailSenderService.Backend.Domain.Entities.RegisteredEmail", b =>
                {
                    b.HasOne("EmailSenderService.Backend.Domain.Entities.Email", "Email")
                        .WithMany("RegisteredEmail")
                        .HasForeignKey("EmailId")
                        .HasConstraintName("FK_RegisteredEmail_Email")
                        .IsRequired();

                    b.HasOne("EmailSenderService.Backend.Domain.Entities.User", "User")
                        .WithMany("RegisteredEmail")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_RegisteredEmail_User")
                        .IsRequired();

                    b.Navigation("Email");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmailSenderService.Backend.Domain.Entities.Email", b =>
                {
                    b.Navigation("History");

                    b.Navigation("RegisteredEmail");
                });

            modelBuilder.Entity("EmailSenderService.Backend.Domain.Entities.User", b =>
                {
                    b.Navigation("History");

                    b.Navigation("RegisteredEmail");
                });
#pragma warning restore 612, 618
        }
    }
}

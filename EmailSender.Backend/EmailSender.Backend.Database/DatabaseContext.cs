namespace EmailSender.Backend.Database;

using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public virtual DbSet<Emails> Emails { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<EmailsHistory> EmailsHistory { get; set; }

    public virtual DbSet<UserEmails> UserEmails { get; set; }

    public virtual DbSet<UserDomains> UserDomains { get; set; }

    public virtual DbSet<Pricing> Pricing { get; set; }

    public virtual DbSet<Billings> Billings { get; set; }

    public virtual DbSet<RequestsHistory> RequestsHistory { get; set; }
        
    public virtual DbSet<StandardPricing> StandardPricing { get; set; }

    public virtual DbSet<UserDetails> UserDetails { get; set; }

    public virtual DbSet<VatNumberPatterns> VatNumberPatterns { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ApplyConfiguration(modelBuilder);
    }

    private static void ApplyConfiguration(ModelBuilder modelBuilder) 
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
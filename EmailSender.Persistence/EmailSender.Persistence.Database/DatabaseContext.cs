using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using EmailSender.Backend.Domain.Entities;

namespace EmailSender.Persistence.Database;

[ExcludeFromCodeCoverage]
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public virtual DbSet<Emails> Emails { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<SentHistory> SentHistory { get; set; }

    public virtual DbSet<UserEmails> UserEmails { get; set; }

    public virtual DbSet<UserAllowedIps> UserAllowedIps { get; set; }

    public virtual DbSet<RequestsHistory> RequestsHistory { get; set; }

    public virtual DbSet<UserDetails> UserDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ApplyConfiguration(modelBuilder);
    }

    private static void ApplyConfiguration(ModelBuilder modelBuilder) 
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
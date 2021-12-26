namespace EmailSender.Backend.Database
{
    using System.Reflection;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public virtual DbSet<Email> Email { get; set; }

        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<EmailHistory> EmailHistory { get; set; }

        public virtual DbSet<AllowEmail> AllowEmail { get; set; }

        public virtual DbSet<AllowDomain> AllowDomain { get; set; }

        public virtual DbSet<Pricing> Pricing { get; set; }

        public virtual DbSet<Billing> Billing { get; set; }

        public virtual DbSet<RequestHistory> RequestHistory { get; set; }
        
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
}
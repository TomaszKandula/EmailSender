using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Persistence.Database.Initializer;

[ExcludeFromCodeCoverage]
public class DbInitializer : IDbInitializer
{
    private readonly DatabaseContext _databaseContext;

    public DbInitializer(DatabaseContext databaseContext) => _databaseContext = databaseContext;

    public void StartMigration() => _databaseContext.Database.Migrate();

    public void SeedData()
    {
        // Seed here
    }
}
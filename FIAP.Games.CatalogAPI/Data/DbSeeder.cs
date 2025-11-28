using FIAP.Games.CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Games.CatalogAPI.Data;

public class DbSeeder
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<DbSeeder> _logger;

    public DbSeeder(CatalogDbContext context, ILogger<DbSeeder> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting database migration");
        await _context.Database.MigrateAsync(cancellationToken);

        if (await _context.Games.AnyAsync(cancellationToken))
        {
            _logger.LogInformation("Database already seeded");
            return;
        }

        _logger.LogInformation("Seeding initial games");

        var seedGames = new List<Game>
        {
            new()
            {
                Title = "Sky Legends",
                Description = "Arcade shooter multiplayer",
                Genre = "Action",
                Price = 59.9m,
                ReleaseDate = new DateOnly(2023, 11, 10)
            },
            new()
            {
                Title = "Mystic Valley",
                Description = "Narrative RPG",
                Genre = "RPG",
                Price = 89.0m,
                ReleaseDate = new DateOnly(2024, 3, 27)
            }
        };

        await _context.Games.AddRangeAsync(seedGames, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Database seeding completed");
    }
}

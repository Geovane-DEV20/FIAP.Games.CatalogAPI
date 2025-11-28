using FIAP.Games.CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Games.CatalogAPI.Data;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
    }

    public DbSet<Game> Games => Set<Game>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GameEntityConfiguration());
    }

    private sealed class GameEntityConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Games");
            builder.HasKey(game => game.Id);

            builder.Property(game => game.Title)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(game => game.Description)
                .HasMaxLength(500);

            builder.Property(game => game.Genre)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(game => game.Price)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(game => game.ReleaseDate)
                .HasColumnType("date")
                .HasConversion(
                    date => date.ToDateTime(TimeOnly.MinValue),
                    value => DateOnly.FromDateTime(value));

            builder.Property(game => game.IsAvailable)
                .HasDefaultValue(true);

            builder.Property(game => game.CreatedAtUtc)
                .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.Property(game => game.UpdatedAtUtc)
                .HasDefaultValueSql("SYSUTCDATETIME()");
        }
    }
}

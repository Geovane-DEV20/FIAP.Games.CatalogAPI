using FIAP.Games.CatalogAPI.Data;
using FIAP.Games.CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Games.CatalogAPI.Repositories;

public class GameRepository : IGameRepository
{
    private readonly CatalogDbContext _context;

    public GameRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Game game, CancellationToken cancellationToken)
    {
        await _context.Games.AddAsync(game, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Games.FindAsync(new object[] { id }, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _context.Games.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IReadOnlyCollection<Game>> GetAllAsync(string? search, string? genre, CancellationToken cancellationToken)
    {
        IQueryable<Game> query = _context.Games.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(game =>
                EF.Functions.Like(game.Title, $"%{term}%") ||
                (game.Description != null && EF.Functions.Like(game.Description, $"%{term}%")));
        }

        if (!string.IsNullOrWhiteSpace(genre))
        {
            var normalizedGenre = genre.Trim();
            query = query.Where(game => game.Genre == normalizedGenre);
        }

        var result = await query
            .OrderBy(game => game.Title)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Games.AsNoTracking().FirstOrDefaultAsync(game => game.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Game>> SearchByTitleAsync(string title, CancellationToken cancellationToken)
    {
        var normalized = title.Trim();
        if (string.IsNullOrWhiteSpace(normalized))
        {
            return Array.Empty<Game>();
        }

        return await _context.Games
            .AsNoTracking()
            .Where(game => EF.Functions.Like(game.Title, $"%{normalized}%"))
            .OrderBy(game => game.Title)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Game game, CancellationToken cancellationToken)
    {
        _context.Games.Update(game);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

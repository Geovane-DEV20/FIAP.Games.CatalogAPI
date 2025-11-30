using FIAP.Games.CatalogAPI.Models;

namespace FIAP.Games.CatalogAPI.Repositories;

public interface IGameRepository
{
    Task<IReadOnlyCollection<Game>> GetAllAsync(string? search, string? genre, CancellationToken cancellationToken);
    Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Game>> SearchByTitleAsync(string title, CancellationToken cancellationToken);
    Task AddAsync(Game game, CancellationToken cancellationToken);
    Task UpdateAsync(Game game, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

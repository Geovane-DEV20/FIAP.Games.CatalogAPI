using FIAP.Games.CatalogAPI.Dtos;

namespace FIAP.Games.CatalogAPI.Services;

public interface IGameService
{
    Task<IReadOnlyCollection<GameResponseDto>> GetAsync(string? search, string? genre, CancellationToken cancellationToken);
    Task<GameResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<GameResponseDto> CreateAsync(GameCreateDto dto, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Guid id, GameUpdateDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

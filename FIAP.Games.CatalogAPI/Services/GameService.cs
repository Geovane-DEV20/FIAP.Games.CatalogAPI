using FIAP.Games.CatalogAPI.Dtos;
using FIAP.Games.CatalogAPI.Extensions;
using FIAP.Games.CatalogAPI.Repositories;

namespace FIAP.Games.CatalogAPI.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _repository;

    public GameService(IGameRepository repository)
    {
        _repository = repository;
    }

    public async Task<GameResponseDto> CreateAsync(GameCreateDto dto, CancellationToken cancellationToken)
    {
        var game = dto.CreateFrom();
        await _repository.AddAsync(game, cancellationToken);
        return game.ToResponseDto();
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<GameResponseDto>> GetAsync(string? search, string? genre, CancellationToken cancellationToken)
    {
        var games = await _repository.GetAllAsync(search, genre, cancellationToken);
        return games.Select(game => game.ToResponseDto()).ToList();
    }

    public async Task<GameResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var game = await _repository.GetByIdAsync(id, cancellationToken);
        return game?.ToResponseDto();
    }

    public async Task<IReadOnlyCollection<GameResponseDto>> SearchByTitleAsync(string title, CancellationToken cancellationToken)
    {
        var games = await _repository.SearchByTitleAsync(title, cancellationToken);
        return games.Select(game => game.ToResponseDto()).ToList();
    }

    public async Task<bool> UpdateAsync(Guid id, GameUpdateDto dto, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        existing.ApplyUpdate(dto);
        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }
}

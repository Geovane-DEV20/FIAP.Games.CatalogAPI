using FIAP.Games.CatalogAPI.Dtos;
using FIAP.Games.CatalogAPI.Models;

namespace FIAP.Games.CatalogAPI.Extensions;

public static class GameMappingExtensions
{
    public static GameResponseDto ToResponseDto(this Game game) => new()
    {
        Id = game.Id,
        Title = game.Title,
        Description = game.Description,
        Genre = game.Genre,
        Price = game.Price,
        ReleaseDate = game.ReleaseDate,
        IsAvailable = game.IsAvailable,
        CreatedAtUtc = game.CreatedAtUtc,
        UpdatedAtUtc = game.UpdatedAtUtc
    };

    public static void ApplyUpdate(this Game game, GameUpdateDto dto)
    {
        game.Title = dto.Title;
        game.Description = dto.Description;
        game.Genre = dto.Genre;
        game.Price = dto.Price;
        game.ReleaseDate = dto.ReleaseDate;
        game.IsAvailable = dto.IsAvailable;
        game.UpdatedAtUtc = DateTime.UtcNow;
    }

    public static Game CreateFrom(this GameCreateDto dto) => new()
    {
        Title = dto.Title,
        Description = dto.Description,
        Genre = dto.Genre,
        Price = dto.Price,
        ReleaseDate = dto.ReleaseDate,
        IsAvailable = dto.IsAvailable,
        CreatedAtUtc = DateTime.UtcNow,
        UpdatedAtUtc = DateTime.UtcNow
    };
}

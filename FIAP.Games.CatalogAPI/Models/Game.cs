using System.ComponentModel.DataAnnotations;

namespace FIAP.Games.CatalogAPI.Models;

public class Game
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(120)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(60)]
    public string Genre { get; set; } = string.Empty;

    [Range(0, 999.99)]
    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public bool IsAvailable { get; set; } = true;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
}

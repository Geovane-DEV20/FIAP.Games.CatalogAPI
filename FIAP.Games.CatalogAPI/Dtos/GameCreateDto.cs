using System.ComponentModel.DataAnnotations;

namespace FIAP.Games.CatalogAPI.Dtos;

public class GameCreateDto
{
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

    [Required]
    public DateOnly ReleaseDate { get; set; }

    public bool IsAvailable { get; set; } = true;
}

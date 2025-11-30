using System.ComponentModel.DataAnnotations;
using FIAP.Games.CatalogAPI.Dtos;
using FIAP.Games.CatalogAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.Games.CatalogAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }
    /// <summary>
    /// Obtém todos os jogos com filtros opcionais
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GameResponseDto>>> GetAsync(
        [FromQuery] string? search,
        [FromQuery] string? genre,
        CancellationToken cancellationToken)
    {
        var games = await _gameService.GetAsync(search, genre, cancellationToken);
        return Ok(games);
    }

    [HttpGet("search")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GameResponseDto>>> SearchByTitleAsync(
        [FromQuery][Required] string title,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return BadRequest("O título é obrigatório para realizar a pesquisa.");
        }

        var games = await _gameService.SearchByTitleAsync(title, cancellationToken);
        return Ok(games);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GameResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var game = await _gameService.GetByIdAsync(id, cancellationToken);
        return game is null ? NotFound() : Ok(game);
    }

   
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GameResponseDto>> CreateAsync([FromBody] GameCreateDto dto, CancellationToken cancellationToken)
    {
        var created = await _gameService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] GameUpdateDto dto, CancellationToken cancellationToken)
    {
        var updated = await _gameService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _gameService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
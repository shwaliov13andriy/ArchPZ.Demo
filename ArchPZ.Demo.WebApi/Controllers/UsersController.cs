namespace ArchPZ.Demo.WebApi.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ArchPZ.Demo.Application.DTOs;
using ArchPZ.Demo.Application.UserManagement.Commands;
using ArchPZ.Demo.Application.UserManagement.Queries;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id), cancellationToken);

        if (user is null)
            return NotFound(new { Message = $"Користувача з Id '{id}' не знайдено." });

        return Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> Create(
        [FromBody] CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            return BadRequest(new { ex.Message });
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Handlers.User.Commands.Create;
using WebApi.Handlers.User.Commands.Delete;
using WebApi.Handlers.User.Commands.Update;
using WebApi.Handlers.User.Queries.GetById;
using WebApi.Handlers.User.Queries.GetUsers;

namespace WebApi.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("by-id")]
    public async Task<IActionResult> GetAsync([FromQuery] GetByIdRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetUsersAsync([FromBody] GetUsersRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteAsync([FromQuery] DeleteRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response);
    }
}
using Application.Auth;
using Application.Auth.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectire.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterCommand registerCommand)
    {
        var token = await _mediator.Send(registerCommand);
        
        return Ok(token);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginCommand loginCommand)
    {
        var token = await _mediator.Send(loginCommand);

        return Ok(token);
    }
    
}
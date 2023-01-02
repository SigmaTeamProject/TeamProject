using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using Application.Commands.Auth.Login;
using Application.Commands.Auth.Registration;
using Application.Services.Interfaces;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IModeratorService _moderatorService;
    public AuthController(IMediator mediator, IMapper mapper, IModeratorService moderatorService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _moderatorService = moderatorService;
    }

    [HttpPost]
    [ActionName("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto user)
    {
        var command = _mapper.Map<LoginCommand>(user);
        var (customer, token) = await _mediator.Send(command);
        var response = new
        {
            access_token = token,
            customer = customer
        };
        return Ok(response);
    }

    [HttpPost]
    [ActionName("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto user, [FromQuery] string? tkn)
    {
        var command = _mapper.Map<RegisterUserCommand>(user);
        command.Token = tkn;
        var (customer, token) = await _mediator.Send(command);
        var response = new
        {
            access_token = token,
            customer = customer
        };
        return Ok(response);
    }

    [HttpGet("moderator")]
    public async Task<ActionResult> GetModeratorLink()
    {
        var link = _moderatorService.GetModeratorLink();
        return Ok(link);
    }
}
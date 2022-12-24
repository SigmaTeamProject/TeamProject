using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using Application.Dtos;
using Application.Auth;
using Application.Commands.Auth;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ActionName("token")]
        public async Task<IActionResult> Token([FromBody] LoginDto user)
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
        public async Task<IActionResult> Register([FromBody] RegisterUserModel user)
        {
            var command = _mapper.Map<RegisterUserCommand>(user);
            var (customer, token) = await _mediator.Send(command);
            var response = new
            {
                access_token = token,
                customer = customer
            };
            return Json(response);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using Application.Commands.Auth.Login;
using Application.Commands.Auth.Registration;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IMapper _mapper;

        public AuthController(IMapper mapper)
        {
            _mapper = mapper;
        }

         [HttpPost]
         [ActionName("login")]
         public async Task<IActionResult> Token([FromBody] LoginDto user)
         {
             var command = _mapper.Map<LoginCommand>(user);
             var (customer, token) = await Mediator.Send(command);
             var response = new
             {
                 access_token = token,
                 customer = customer
             };
            return Ok(response);
        }

        [HttpPost]
        [ActionName("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto user)
        {
            var command = _mapper.Map<RegisterUserCommand>(user);
            var (customer, token) = await Mediator.Send(command);
            var response = new
            {
                access_token = token,
                customer = customer
            };
            return Ok(response);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace WebApi.Controllers;

public class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    protected int UserId =>
        !User.Identity.IsAuthenticated ? 0 : int.Parse(User.FindFirst(JwtRegisteredClaimNames.Sub)!.Value);
}
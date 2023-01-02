using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    protected int UserId =>
        !User.Identity!.IsAuthenticated ? 0 : int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
}
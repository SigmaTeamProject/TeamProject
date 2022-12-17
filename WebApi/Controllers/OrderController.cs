using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok();
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById (int id)
    {
        return Ok();
    }
}
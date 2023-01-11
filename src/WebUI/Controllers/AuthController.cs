using gql.Application.Common.Interfaces;
using gql.Infrastructure.Identity;
using gql.WebUI.Controllers;
using GraphQL;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers;

[Authorize]
public class AuthController : ApiControllerBase
{
    private readonly IIdentityService _identityService;

    public AuthController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [AllowAnonymous]
    [HttpPost("~/login")]
    public async Task<IActionResult> Login(LoginWithPassword model)
    {
        var result = await _identityService.AuthenticateAsync(model.UserName, model.Password);
        return Ok(result);
    }
}

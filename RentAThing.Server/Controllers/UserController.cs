using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentAThing.Server.Application.Handlers.Commands;
using RentAThing.Server.Application.Handlers.Queries;
using System.IdentityModel.Tokens.Jwt;

namespace RentAThing.Server.Controllers;


[ApiController]
[Route("api/user")]
public class UserController(IMediator mediator) : ControllerBase {

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel login) {
        var resp = await mediator.Send(new LoginCommand(login.Username, login.Password));
        if (resp == null) {
            return Unauthorized();
        }

        return Ok(resp);
    }

    [HttpGet("info")]
    [Authorize]
    public async Task<UserInfoResponse> Info() {
        var  claim = User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub);
        var userInfo = await mediator.Send(new UserInfoQuery(int.Parse(claim.Value)));
        return userInfo;
    }

    [HttpGet("info-admin")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<UserInfoResponse> InfoAdmin() {
        var claim = User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub);
        var userInfo = await mediator.Send(new UserInfoQuery(int.Parse(claim.Value)));
        return userInfo;
    }
}

public class LoginModel {
    public string? Username { get; set; }
    public string? Password { get; set; }
}
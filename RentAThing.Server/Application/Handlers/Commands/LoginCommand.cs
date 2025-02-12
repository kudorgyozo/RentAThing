using MediatR;
using Microsoft.EntityFrameworkCore;
using RentAThing.Server.Application.Services;
using RentAThing.Server.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RentAThing.Server.Application.Handlers.Commands;

public class LoginCommand(string? username, string? password) : IRequest<LoginResponse?> {
    public string? Username { get; } = username;
    public string? Password { get; } = password;
}

public class LoginResponse {
    public required string Token { get; set; }

    public required Dictionary<string, string> Claims { get; set; }
    public long Expires { get; internal set; }
}

public class ClaimDto {
    public string? Type { get; set; }
    public string? Value { get; set; }
}

public class LoginQueryHandler(AppDbContext dbContext, TokenService tokenService) : IRequestHandler<LoginCommand, LoginResponse?> {
    public async Task<LoginResponse?> Handle(LoginCommand request, CancellationToken cancellationToken) {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == request.Username && u.Password == request.Password, cancellationToken);
        if (user == null) {
            return null;
        }

        var (token, claims, expires) = tokenService.GenerateToken(user.UserName, user.Id);

        var response = new LoginResponse {
            Token = token,
            Claims = claims,
            Expires = expires,
        };

        return response;

    }
}



using MediatR;
using Microsoft.EntityFrameworkCore;
using RentAThing.Server.Application.Services;
using RentAThing.Server.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RentAThing.Server.Application.Handlers.Commands;

public class LoginCommand : IRequest<LoginResponse?> {

    public LoginCommand(string? username, string? password) {
        Username = username;
        Password = password;
    }

    public string? Username { get; }
    public string? Password { get; }
}

public class LoginResponse {
    public required string Token { get; set; }

    public required Dictionary<string, string> Claims { get; set; }
}

public class ClaimDto {
    public string? Type { get; set; }
    public string? Value { get; set; }
}

public class LoginQueryHandler : IRequestHandler<LoginCommand, LoginResponse?> {
    private readonly AppDbContext dbContext;
    private readonly TokenService tokenService;

    public LoginQueryHandler(AppDbContext dbContext, TokenService tokenService) {
        this.dbContext = dbContext;
        this.tokenService = tokenService;
    }

    public async Task<LoginResponse?> Handle(LoginCommand request, CancellationToken cancellationToken) {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == request.Username && u.Password == request.Password);
        if (user == null) {
            return null;
        }

        var (token, claims) = tokenService.GenerateToken(user.UserName, user.Id);

        var response = new LoginResponse {
            Token = token,
            Claims = claims
        };

        return response;

    }
}



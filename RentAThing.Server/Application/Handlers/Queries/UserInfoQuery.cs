using MediatR;
using Microsoft.EntityFrameworkCore;
using RentAThing.Server.Application.Services;
using RentAThing.Server.Infrastructure;

namespace RentAThing.Server.Application.Handlers.Queries;

public class UserInfoResponse {
    public string? Username { get; set; }

    public int Id { get; set; }
}

public class UserInfoQuery(int userId) : IRequest<UserInfoResponse> {
    public int UserId { get; } = userId;
}

public class UserInfoQueryHandler(AppDbContext dbContext) : IRequestHandler<UserInfoQuery, UserInfoResponse> {
    private readonly AppDbContext dbContext = dbContext;

    public async Task<UserInfoResponse> Handle(UserInfoQuery request, CancellationToken cancellationToken) {
        var user = await dbContext.Users.FirstAsync(u => u.Id == request.UserId, cancellationToken);
        return new UserInfoResponse {
            Username = user.UserName,
            Id = user.Id,
        };

    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using RentAThing.Server.Application.Exceptions;
using RentAThing.Server.Application.Interfaces;
using RentAThing.Server.Infrastructure;
using RentAThing.Server.Models;

namespace RentAThing.Server.Application.Handlers.Commands; 

public class StartRentCommand(int userId, int itemId) : IRequest {
    public int UserId { get; } = userId;
    public int ItemId { get; } = itemId;
}

public class StartRentCommandHandler(IRentRepo rentRepo) : IRequestHandler<StartRentCommand> {
    public async Task Handle(StartRentCommand request, CancellationToken cancellationToken) {
        await rentRepo.StartRent(request.UserId, request.ItemId, cancellationToken);

    }

}

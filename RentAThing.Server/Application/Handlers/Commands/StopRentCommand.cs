using MediatR;
using RentAThing.Server.Application.Interfaces;

namespace RentAThing.Server.Application.Handlers.Commands;
public class StopRentCommand(int userId, int itemId) : IRequest {
    public int UserId { get; } = userId;
    public int ItemId { get; } = itemId;
}

public class StopRentCommandHandler(IRentRepo rentRepo) : IRequestHandler<StopRentCommand> {
    public async Task Handle(StopRentCommand request, CancellationToken cancellationToken) {
        await rentRepo.StopRent(request.UserId, request.ItemId, cancellationToken);
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using RentAThing.Server.Application.Exceptions;
using RentAThing.Server.Infrastructure;

namespace RentAThing.Server.Application.Handlers.Commands; 

public class StartRentCommand(int userId, int itemId) : IRequest {
    public int UserId { get; } = userId;
    public int ItemId { get; } = itemId;
}

public class StartRentCommandHandler(AppDbContext context) : IRequestHandler<StartRentCommand> {
    public async Task Handle(StartRentCommand request, CancellationToken cancellationToken) {
        var item = await context.RentalItems.FirstAsync(i => i.Id == request.ItemId, cancellationToken);
        if (item.RenterId != null) {
            throw new RentAlreadyStartedException($"Rent already started: user: {item.RenterId} start: {item.RentStart}", null);
        }
        item.RenterId = request.UserId;
        item.RentStart = DateTime.UtcNow;
        await context.SaveChangesAsync(cancellationToken);

    }

}

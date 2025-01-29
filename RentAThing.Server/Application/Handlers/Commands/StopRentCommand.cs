using MediatR;
using Microsoft.EntityFrameworkCore;
using RentAThing.Server.Application.Exceptions;
using RentAThing.Server.Infrastructure;

namespace RentAThing.Server.Application.Handlers.Commands;
public class StopRentCommand(int userId, int itemId) : IRequest {
    public int UserId { get; } = userId;
    public int ItemId { get; } = itemId;
}

public class StopRentCommandHandler(AppDbContext context) : IRequestHandler<StopRentCommand> {
    public async Task Handle(StopRentCommand request, CancellationToken cancellationToken) {
        var item = await context.RentalItems.FirstAsync(i => i.Id == request.ItemId && i.RenterId == request.UserId, cancellationToken);
        if (item == null) {
            throw new RentNeverStartedException($"Rent never started. item: {request.ItemId} user: {request.UserId}");
        }
        item.RenterId = null;
        item.RentStart = null;

        var history = context.RentHistory.First(h => h.RenterId == request.UserId && h.ItemId == request.ItemId);
        history.RentEnd = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);
    }

    //Task IRequestHandler<StopRentCommand>.Handle(StopRentCommand request, CancellationToken cancellationToken) {
    //    throw new NotImplementedException();
    //}
}

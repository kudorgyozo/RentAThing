using MediatR;
using RentAThing.Server.Models;
using Microsoft.EntityFrameworkCore;
using RentAThing.Server.Infrastructure;

namespace RentAThing.Server.Application.Handlers.Queries;

public class GetRentalItemsQuery(bool availableOnly) : IRequest<IEnumerable<ItemDto>> {
    public bool AvailableOnly { get; set; } = availableOnly;
}

public class GetRentalItemsQueryHandler(AppDbContext context) : IRequestHandler<GetRentalItemsQuery, IEnumerable<ItemDto>> {
    public async Task<IEnumerable<ItemDto>> Handle(GetRentalItemsQuery request, CancellationToken cancellationToken) {
        var q = context.RentalItems.Include(ri => ri.Renter).AsQueryable();
        if (request.AvailableOnly) {
            q = q.Where(ri => ri.RenterId == null);
        }
        var res = await q.ToListAsync(cancellationToken);
        return res.Select(ri => new ItemDto {
            Id = ri.Id,
            Name = ri.Name,
            PricePerHour = ri.PricePerHour,
            Renter = ri.Renter?.UserName,
            RentStart = ri.RentStart?.ToLocalTime(),
            Type = ri.Type
        });

    }
}
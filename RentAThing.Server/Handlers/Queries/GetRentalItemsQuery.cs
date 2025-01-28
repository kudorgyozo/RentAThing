using MediatR;
using RentAThing.Server.Models;
using Microsoft.EntityFrameworkCore;
using RentAThing.Server.Infrastructure;

namespace RentAThing.Server.Handlers.Queries;

public class GetRentalItemsQuery : IRequest<IEnumerable<RentalItem>> {
    public bool AvailableOnly { get; set; }

    public GetRentalItemsQuery(bool availableOnly) {
        AvailableOnly = availableOnly;
    }
}

public class GetRentalItemsQueryHandler : IRequestHandler<GetRentalItemsQuery, IEnumerable<RentalItem>> {
    private readonly AppDbContext _context;

    public GetRentalItemsQueryHandler(AppDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<RentalItem>> Handle(GetRentalItemsQuery request, CancellationToken cancellationToken) {
        var q = _context.RentalItems.AsQueryable();
        if (request.AvailableOnly) {
            q = q.Where(ri => ri.RenterId == null);
        }
        return await q.ToListAsync(cancellationToken);

    }
}
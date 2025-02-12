using MediatR;
using RentAThing.Server.Application.Handlers;
using RentAThing.Server.Application.Interfaces;
using RentAThing.Server.Infrastructure;

namespace RentAThing.Server.Application.Handlers.Queries {
    public class GetActiveItemsQuery(int userId) : IRequest<IEnumerable<ItemDto>> {
        public int UserId { get; } = userId;
    }

    public class GetActiveItemsQueryHandler(IRentRepo rentRepo) : IRequestHandler<GetActiveItemsQuery, IEnumerable<ItemDto>> {
        private readonly IRentRepo rentRepo = rentRepo;

        public async Task<IEnumerable<ItemDto>> Handle(GetActiveItemsQuery request, CancellationToken cancellationToken) {
            var items = await rentRepo.GetActiveItems(request.UserId);
            return items.Select(x => new ItemDto { 
                Id = x.Id, 
                Name = x.Name,
                Renter = x.Renter!.UserName,
                Type = x.Type,
                PricePerHour = x.PricePerHour,
                RentStart = x.RentStart,
            });
        }
    }
}
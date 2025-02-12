using MediatR;
using Microsoft.EntityFrameworkCore;
using RentAThing.Server.Application.Exceptions;
using RentAThing.Server.Application.Interfaces;
using RentAThing.Server.Models;
using System.Threading;

namespace RentAThing.Server.Infrastructure {
    public class RentRepo(AppDbContext dbContext) : IRentRepo {
        private readonly AppDbContext dbContext = dbContext;

        public async Task<List<RentalItem>> GetActiveItems(int userId) {
            var items = await dbContext.RentalItems.Include(ri => ri.Renter).Where(ri => ri.RenterId == userId).ToListAsync();
            return items;
        }

        public async Task StartRent(int userId, int itemId, CancellationToken cancellationToken) {
            var item = await dbContext.RentalItems.FirstAsync(i => i.Id == itemId, cancellationToken);
            if (item.RenterId != null) {
                throw new RentAlreadyStartedException($"Rent already started: user: {item.RenterId} start: {item.RentStart}", null);
            }
            item.RenterId = userId;
            item.RentStart = DateTime.UtcNow;
            dbContext.RentHistory.Add(new RentHistory {
                ItemId = item.Id,
                RenterId = userId,
                RentEvent = RentEvent.Start,
                EventDate = item.RentStart.Value,
            });
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task StopRent(int userId, int itemId, CancellationToken cancellationToken) {
            var item = await dbContext.RentalItems.FirstAsync(i => i.Id == itemId && i.RenterId == userId, cancellationToken) 
                ?? throw new RentNeverStartedException($"Rent never started. item: {itemId} user: {userId}");
            item.RenterId = null;
            item.RentStart = null;

            var history = new RentHistory() {
                RenterId = userId,
                ItemId = itemId,
                EventDate = DateTime.UtcNow,
                RentEvent = RentEvent.Stop
            };
            dbContext.RentHistory.Add(history);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

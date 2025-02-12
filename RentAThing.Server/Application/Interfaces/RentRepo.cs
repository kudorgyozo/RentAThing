using RentAThing.Server.Models;

namespace RentAThing.Server.Application.Interfaces {
    public interface IRentRepo {
        public Task<List<RentalItem>> GetActiveItems(int userId);
        public Task StartRent(int userId, int itemId, CancellationToken token);

        public Task StopRent(int userId, int itemId, CancellationToken token);
    }
}

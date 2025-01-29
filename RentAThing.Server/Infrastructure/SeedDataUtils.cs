using RentAThing.Server.Models;

namespace RentAThing.Server.Infrastructure {
    public class SeedDataUtils {

        public static void AddSeedData(AppDbContext dbContext) {
            dbContext.Users.AddRange([
                    // test users
                    new User { Id = 1, UserName = "admin", Password = "admin" },
                    new User { Id = 2, UserName = "gyozo", Password = "gyozo" },
                    new User { Id = 3, UserName = "user1", Password = "user1" },
                    new User { Id = 4, UserName = "user2", Password = "user2" },
                    new User { Id = 5, UserName = "user3", Password = "user3" }
                ]);

            dbContext.Cars.AddRange([
                    new Car { Id = 1, Name = "Car 1", Make = "Make 1", Model = "Model 1", Year = 2010, PricePerHour = 10 },
                    new Car { Id = 2, Name = "Car 2", Make = "Make 2", Model = "Model 2", Year = 2015, PricePerHour = 15 },
                    new Car { Id = 3, Name = "Car 3", Make = "Make 3", Model = "Model 3", Year = 2020, PricePerHour = 20, RenterId = 1, RentStart = DateTime.UtcNow }
                ]);
            dbContext.Bikes.AddRange([
                    new Bike { Id = 4, Name = "Bike 1", Type = "Mountain", PricePerHour = 5 },
                    new Bike { Id = 5, Name = "Bike 2", Type = "Road", PricePerHour = 6 },
                    new Bike { Id = 6, Name = "Bike 3", Type = "Hybrid", PricePerHour = 7 }
                ]);
            dbContext.ElectricScooters.AddRange([
                    new ElectricScooter { Id = 7, Name = "Scooter 1", Range = 25, PricePerHour = 8 },
                    new ElectricScooter { Id = 8, Name = "Scooter 2", Range = 50, PricePerHour = 9 },
                    new ElectricScooter { Id = 9, Name = "Scooter 3", Range = 75, PricePerHour = 10 }
                ]);
            dbContext.SaveChanges();
        }

        public static void DropCreateDB(WebApplication app) {
            using var scope = app.Services.CreateScope();
            //////
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            var allRentalItems = dbContext.RentalItems.ToList();
            var allCars = dbContext.Cars.ToList();
            var allBikes = dbContext.Bikes.ToList();
            var allElectricScooters = dbContext.ElectricScooters.ToList();
        }
    }
}

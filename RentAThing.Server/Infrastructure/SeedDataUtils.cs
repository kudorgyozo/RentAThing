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
                    new Car {Type = ItemType.Car, Id = 1, Name = "Car 1", Make = "Make 1", Model = "Model 1", Year = 2010, PricePerHour = 10 },
                    new Car {Type = ItemType.Car, Id = 2, Name = "Car 2", Make = "Make 2", Model = "Model 2", Year = 2015, PricePerHour = 15 },
                    new Car {Type = ItemType.Car, Id = 3, Name = "Car 3", Make = "Make 3", Model = "Model 3", Year = 2020, PricePerHour = 20 }
                ]);
            dbContext.Bikes.AddRange([
                    new Bike {Type = ItemType.Bike, Id = 4, Name = "Bike 1", BikeType = "Mountain", PricePerHour = 5 },
                    new Bike {Type = ItemType.Bike, Id = 5, Name = "Bike 2", BikeType = "Road", PricePerHour = 6 },
                    new Bike {Type = ItemType.Bike, Id = 6, Name = "Bike 3", BikeType = "Hybrid", PricePerHour = 7 }
                ]);
            dbContext.ElectricScooters.AddRange([
                    new ElectricScooter {Type = ItemType.ElectricScooter, Id = 7, Name = "Scooter 1", Range = 25, PricePerHour = 8 },
                    new ElectricScooter {Type = ItemType.ElectricScooter, Id = 8, Name = "Scooter 2", Range = 50, PricePerHour = 9 },
                    new ElectricScooter {Type = ItemType.ElectricScooter, Id = 9, Name = "Scooter 3", Range = 75, PricePerHour = 10 }
                ]);

            dbContext.RentHistory.Add(new RentHistory {
                ItemId = 1,
                RenterId = 1,
                EventDate = new DateTime(2021, 1, 1, 10, 0, 0),
                RentEvent = RentEvent.Start
            });

            dbContext.RentHistory.Add(new RentHistory {
                ItemId = 1,
                RenterId = 1,
                EventDate = new DateTime(2021, 1, 2, 10, 0, 0),
                RentEvent = RentEvent.Stop
            });

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

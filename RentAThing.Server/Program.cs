using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RentAThing.Server.Endpoints;
using RentAThing.Server.Handlers.Queries;
using RentAThing.Server.Infrastructure;
using RentAThing.Server.Models;
using System;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(opt => {
    opt.UseSqlite(builder.Configuration.GetConnectionString("SqLite"))
        //.UseSeeding((context, _) => {
        //    context.Set<User>().AddRange(new User[] {
        //        // test users
        //        new User { Id = 1, UserName = "user1", Password = "password1" },
        //        new User { Id = 2, UserName = "user2", Password = "password2" },
        //        new User { Id = 3, UserName = "user3", Password = "password3" }
        //    });

        //    context.Set<Car>().AddRange(new Car[] {
        //        new Car { Id = 1, Name = "Car 1", Make = "Make 1", Model = "Model 1", Year = 2010, PricePerHour = 10 },
        //        new Car { Id = 2, Name = "Car 2", Make = "Make 2", Model = "Model 2", Year = 2015, PricePerHour = 15 },
        //        new Car { Id = 3, Name = "Car 3", Make = "Make 3", Model = "Model 3", Year = 2020, PricePerHour = 20, RenterId = 1 }
        //    });
        //    context.Set<Bike>().AddRange(new Bike[] {
        //        new Bike { Id = 4, Name = "Bike 1", Type = "Mountain", PricePerHour = 5 },
        //        new Bike { Id = 5, Name = "Bike 2", Type = "Road", PricePerHour = 6 },
        //        new Bike { Id = 6, Name = "Bike 3", Type = "Hybrid", PricePerHour = 7 }
        //    });
        //    context.Set<ElectricScooter>().AddRange(new ElectricScooter[] {
        //        new ElectricScooter { Id = 7, Name = "Scooter 1", Range = 25, PricePerHour = 8 },
        //        new ElectricScooter { Id = 8, Name = "Scooter 2", Range = 50, PricePerHour = 9 },
        //        new ElectricScooter { Id = 9, Name = "Scooter 3", Range = 75, PricePerHour = 10 }
        //    });
        //    context.SaveChanges();
        //})
        ;
});

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("SqLite")));
// Add MediatR
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(Program)));

var app = builder.Build();

//using (var scope = app.Services.CreateScope()) {
//    //////
//    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    dbContext.Database.EnsureDeleted();
//    dbContext.Database.EnsureCreated();

//    var allRentalItems = dbContext.RentalItems.ToList();
//    var allCars = dbContext.Cars.ToList();
//    var allBikes = dbContext.Bikes.ToList();
//    var allElectricScooters = dbContext.ElectricScooters.ToList();
//}

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//buggy, context is not disposed for some reason
//app.RegisterEndpointDefinitions(); //just a silly test with some custom endpoints

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();


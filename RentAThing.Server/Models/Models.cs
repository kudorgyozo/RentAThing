namespace RentAThing.Server.Models;

public abstract class RentalItem {
    public int Id { get; set; }

    public required string Name { get; set; }

    public decimal PricePerHour { get; set; }

    public int? RenterId { get; set; }

    public User? Renter { get; set; }
}

public class Car : RentalItem {
    public required string Make { get; set; }
    public required string Model { get; set; }
    public int Year { get; set; }
}

public class Bike : RentalItem {
    public required string Type { get; set; } // e.g., Mountain, Road
}

public class ElectricScooter : RentalItem {
    public int Range { get; set; }
}

public class User {

    public int Id { get; set; }

    public required string UserName { get; set; }

    public required string Password { get; set; }

    public ICollection<RentalItem> RentalItems { get; set; } = new List<RentalItem>();
}
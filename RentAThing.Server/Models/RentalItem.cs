namespace RentAThing.Server.Models; 
public abstract class RentalItem {
    public int Id { get; set; }

    public required string Name { get; set; }

    public decimal PricePerHour { get; set; }

    public required ItemType Type { get; set; }

    public int? RenterId { get; set; }

    public User? Renter { get; set; }

    public DateTime? RentStart { get; set; }

    public ICollection<RentHistory> History { get; set; } = [];


}

public enum ItemType {
    Car = 1,
    Bike,
    ElectricScooter
}


public class Car : RentalItem {

    public required string Make { get; set; }

    public required string Model { get; set; }

    public int Year { get; set; }

}

public class Bike : RentalItem {
    public required string BikeType { get; set; } // e.g., Mountain, Road
}

public class ElectricScooter : RentalItem {
    public int Range { get; set; }
}

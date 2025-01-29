namespace RentAThing.Server.Models;



public class User {

    public int Id { get; set; }

    public required string UserName { get; set; }

    public required string Password { get; set; }

    public ICollection<RentalItem> RentalItems { get; set; } = new List<RentalItem>();

    public ICollection<RentHistory> RentHistory { get; set; } = new List<RentHistory>();
}
namespace RentAThing.Server.Models; 
public class RentHistory {
    public int Id { get; set; }
    public int ItemId { get; set; }
    public RentalItem? Item { get; set; }
    public int RenterId { get; set; }
    public User? Renter { get; set; }
    public DateTime RentStart { get; set; }
    public DateTime? RentEnd { get; set; }
}

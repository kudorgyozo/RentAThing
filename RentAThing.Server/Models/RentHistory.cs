namespace RentAThing.Server.Models; 
public class RentHistory {
    public int Id { get; set; }
    public int ItemId { get; set; }
    public RentalItem? Item { get; set; }
    public int RenterId { get; set; }
    public User? Renter { get; set; }
    public DateTime EventDate { get; set; }
    public RentEvent RentEvent { get; set; }
}

public enum RentEvent  {
    Start = 1,
    Stop = 2
}

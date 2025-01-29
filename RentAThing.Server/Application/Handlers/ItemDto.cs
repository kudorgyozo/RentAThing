﻿using RentAThing.Server.Models;

namespace RentAThing.Server.Application.Handlers {
    public class ItemDto {
        public int Id { get; set; }

        public required string Name { get; set; }

        public decimal PricePerHour { get; set; }

        public string? Renter { get; set; }

        public DateTime? RentStart { get; set; }

    }
}

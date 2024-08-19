﻿namespace TicketApi.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } // "Kiosk" or "Online"
    }
}

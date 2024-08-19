using System;

namespace TicketAPI.Models
{
    public class Ticket
    {
        public int TicketId { get; set; } // Primary Key
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } // Kiosk, Online
    }
}

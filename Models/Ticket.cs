using System;
using System.ComponentModel.DataAnnotations;

namespace TicketAPI.Models
{
    public enum TicketType
    {
        Online = 1,
        Kiosk = 2
    }
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; } // Primary Key
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public TicketType Type { get; set; } // Online, Kiosk
    }
}

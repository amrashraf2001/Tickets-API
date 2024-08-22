using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketAPI.Data;
using TicketAPI.Models;
using System.Linq;

namespace TicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TicketContext _context;

        public TicketsController(TicketContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _context.Tickets.ToListAsync();
        }

        // GET: api/Tickets/type/Kiosk
        [HttpGet("type/{type}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketsByType(TicketType type)
        {
            return await _context.Tickets.Where(t => t.Type == type).ToListAsync();
        }

        // GET: api/Tickets/getTicketsTotalPrices
        [HttpGet("getTicketsTotalPrices")]
        public async Task<ActionResult<decimal>> GetTicketsTotalPrices()
        {
            var totalPrices = await _context.Tickets.SumAsync(t => t.Price);
            return totalPrices;
        }

        // GET: api/Tickets/getTicketsTotalPricesByType/Kiosk
        [HttpGet("getTicketsTotalPricesByType/{type}")]
        public async Task<ActionResult<decimal>> GetTicketsTotalPricesByType(TicketType type)
        {
            var totalPrices = await _context.Tickets.Where(t => t.Type == type).SumAsync(t => t.Price);
            return totalPrices;
        }

        // GET: api/Tickets/getTicketsCount
        [HttpGet("getTicketsCount")]
        public async Task<ActionResult<int>> GetTicketsCount()
        {
            var count = await _context.Tickets.CountAsync();
            return count;
        }

        // GET: api/Tickets/getTicketsCountByType/Kiosk
        [HttpGet("getTicketsCountByType/{type}")]
        public async Task<ActionResult<int>> GetTicketsCountByType(TicketType type)
        {
            var count = await _context.Tickets.Where(t => t.Type == type).CountAsync();
            return count;
        }

        // GET: api/Tickets/getTodayTicketsCount
        [HttpGet("getTodayTicketsCount")]
        public async Task<ActionResult<int>> GetTodayTicketsCount()
        {
            var day = DateTime.Today.Day;
            var month = DateTime.Today.Month;
            var year = DateTime.Today.Year;
            var count = await _context.Tickets.Where(t => t.Date.Day == day && t.Date.Month == month && t.Date.Year == year).CountAsync();
            return count;
        }

        // Get: api/Tickets/getTodayTicketsPrices
        [HttpGet("getTodayTicketsPrices")]
        public async Task<ActionResult<decimal>> GetTodayTicketsPrices()
        {
            var day = DateTime.Today.Day;
            var month = DateTime.Today.Month;
            var year = DateTime.Today.Year;
            var totalPrices = await _context.Tickets.Where(t => t.Date.Day == day && t.Date.Month == month && t.Date.Year == year).SumAsync(t => t.Price);
            return totalPrices;
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/Tickets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tickets
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.TicketId }, ticket);
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.TicketId == id);
        }
    }
}

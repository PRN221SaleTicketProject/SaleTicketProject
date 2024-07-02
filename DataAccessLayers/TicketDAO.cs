using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class TicketDAO : GenericDAO<Ticket>
    {
        public int GetRemainingTicketsForEvent(int eventId)
        {
            var totalTicketQuantity = _context.Tickets
                .Where(t => t.EventId == eventId)
                .Sum(t => t.Quantity ?? 0);

            var soldTicketQuantity = _context.SolvedTickets
                .Where(st => st.Ticket!.EventId == eventId)
                .Sum(st => st.Quantity ?? 0);

            var remainingTickets = totalTicketQuantity - soldTicketQuantity;

            return remainingTickets;
        }

        public void UpdateNew(Ticket ticket)
        {
            if (_context.Entry(ticket).State == EntityState.Detached)
            {
                _context.Tickets.Attach(ticket);
            }
            _context.Entry(ticket).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}

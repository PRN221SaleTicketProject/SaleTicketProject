using BusinessObjects;
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
    }
}

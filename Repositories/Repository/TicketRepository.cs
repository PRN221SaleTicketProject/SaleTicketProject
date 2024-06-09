using BusinessObjects;
using DataAccessLayers;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        private readonly GenericDAO<Ticket> _ticketDAO;

        public TicketRepository(GenericDAO<Ticket> ticketDAO) : base(ticketDAO)
        {
            _ticketDAO = ticketDAO;
        }
    }
}

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
    public class SolvedTicketRepository : GenericRepository<SolvedTicket>, ISolvedTicketRepository
    {
        private readonly GenericDAO<SolvedTicket> _solvedTicketDAO;

        public SolvedTicketRepository(GenericDAO<SolvedTicket> solvedTicketDAO) : base(solvedTicketDAO)
        {
            _solvedTicketDAO = solvedTicketDAO;
        }
    }
}

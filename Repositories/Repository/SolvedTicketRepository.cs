using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;

        public SolvedTicketRepository(GenericDAO<SolvedTicket> solvedTicketDAO, IUnitOfWork unitOfWork) : base(solvedTicketDAO)
        {
            _solvedTicketDAO = solvedTicketDAO;
            _unitOfWork = unitOfWork;
        }

        public void PurchaseTickets(List<Ticket> tickets, Account account)
        {
            foreach (var ticket in tickets)
            {
                if (ticket.Quantity > 0)
                {
                    ticket.Quantity--;
                    if (ticket.Quantity == 0) ticket.Status = 0;
                    var solvedTicket = new SolvedTicket
                    {
                        AccountId = account.Id,
                        TicketId = ticket.Id,
                        Quantity = 1,
                        TotalPrice = ticket.Price
                    };
                    _unitOfWork.SolvedTicketDAO.Add(solvedTicket);
                    _unitOfWork.TicketDAO.Update(ticket);
                }
            }
        }
    }
}

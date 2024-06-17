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
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        private readonly GenericDAO<Ticket> _ticketDAO;
        private readonly IUnitOfWork _unitOfWork;

        public TicketRepository(GenericDAO<Ticket> ticketDAO, IUnitOfWork unitOfWork) : base(ticketDAO)
        {
            _ticketDAO = ticketDAO;
            _unitOfWork = unitOfWork;
        }

        public int? CountQuantityPeopleJoinEvent(Event eventName)//uoc luong so nguoi tham gia event
        {
            var quantity = eventName.TicketQuantity;
            var currentTicket = _unitOfWork.TicketDAO.GetRemainingTicketsForEvent(eventName.Id);
            return _ = quantity - currentTicket;
        }

        public List<Ticket> GetByEventId(int eventId)
        {
           return _unitOfWork.TicketDAO.Find(a => a.EventId == eventId).ToList();
        } 
    }
}

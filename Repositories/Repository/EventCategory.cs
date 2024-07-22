using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class EventCategory : GenericRepository<Event>, IEventCategory
    {
        private readonly GenericDAO<Event> _eventDAO;
        private readonly IUnitOfWork _unitOfWork;

        public EventCategory(GenericDAO<Event> eventDAO, IUnitOfWork unitOfWork) : base(eventDAO)
        {
            _eventDAO = eventDAO;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Event> GetAllInclude()
        {
            return _unitOfWork.EventDAO.GetAllInclude();
        }
        public IEnumerable<Event> GetAllIncludeType()
        {
            return _unitOfWork.EventDAO.GetAllIncludeType();
        }
    }
}

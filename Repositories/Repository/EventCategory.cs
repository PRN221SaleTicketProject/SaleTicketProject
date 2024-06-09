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
    public class EventCategory : GenericRepository<Event>, IEventCategory
    {
        private readonly GenericDAO<Event> _eventDAO;

        public EventCategory(GenericDAO<Event> eventDAO) : base(eventDAO)
        {
            _eventDAO = eventDAO;
        }
    }
}

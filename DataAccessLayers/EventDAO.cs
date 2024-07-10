using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class EventDAO : GenericDAO<Event>
    {
        public IEnumerable<Event> GetAllInclude()
        {
            return _context.Events.Include(a => a.Sponsor).ToList();
        }
    }
}

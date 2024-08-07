﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        int? CountQuantityPeopleJoinEvent(Event eventName);
        List<Ticket> GetByEventId(int eventId);
        void UpdateNewTicket(Ticket ticket);
    }
}

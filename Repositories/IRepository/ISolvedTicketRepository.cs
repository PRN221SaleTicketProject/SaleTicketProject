﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ISolvedTicketRepository : IGenericRepository<SolvedTicket>
    {
        void PurchaseTickets(List<Ticket> tickets, Account account, int quantity);
        List<SolvedTicket> GetSolvedTicketsByAccountId(int accountId);
        public Boolean CheckSolvedTicket(int ticketId);
    }
}

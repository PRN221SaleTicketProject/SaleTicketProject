using BusinessObjects;
using BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class TransactionHistoryDAO : GenericDAO<TransactionHistory>
    {
        public List<TransactionHistoryDto> GetAllTransactionHistoryByAccountId(int accountId)
        {
            var transactionHistories =  _context.SolvedTickets
                .Where(st => st.AccountId == accountId)
                .SelectMany(st => st.Transactions)
                .Select(t => new TransactionHistoryDto
                {
                    EventName = t.Event.Name,
                    TicketQuantity = t.SolvedTicket.Quantity,
                    TotalPrice = t.SolvedTicket.TotalPrice,
                    Time = t.TransactionHistories.FirstOrDefault().Time,
                    Status = t.Status,
                    TransactionType = t.Type.Name
                })
                .ToList();

            return transactionHistories;
        }
    }
}

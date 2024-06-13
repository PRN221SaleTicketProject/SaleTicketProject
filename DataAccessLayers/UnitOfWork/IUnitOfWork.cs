using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers.UnitOfWork
{
    public interface IUnitOfWork
    {
        public AccountDAO AccountDAO { get; }
        public CategoryDAO CategoryDAO { get; }
        public EventDAO EventDAO { get; }
        public PromotionDAO PromotionDAO { get; }
        public RoleDAO RoleDAO { get; }
        public SolvedTicketDAO SolvedTicketDAO { get; }
        public TicketDAO TicketDAO { get; }
        public TransactionDAO TransactionDAO { get; }
        public TransactionHistoryDAO TransactionHistoryDAO { get; }
        public TransactionTypeDAO TransactionTypeDAO { get; }
        void SaveChanges();

    }
}

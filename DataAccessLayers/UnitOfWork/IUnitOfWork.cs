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
        public GenericDAO<Account> AccountDAO { get; }
        public GenericDAO<Category> CategoryDAO { get; }
        public GenericDAO<Event> EventDAO { get; }
        public GenericDAO<Promotion> PromotionDAO { get; }
        public GenericDAO<Role> RoleDAO { get; }
        public GenericDAO<SolvedTicket> SolvedTicketDAO { get; }
        public GenericDAO<Ticket> TicketDAO { get; }
        public GenericDAO<Transaction> TransactionDAO { get; }
        public GenericDAO<TransactionHistory> TransactionHistoryDAO { get; }
        public GenericDAO<TransactionTypeDAO> TransactionTypeDAO { get; }
        void SaveChanges();

    }
}

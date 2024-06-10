using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private Prn221projectContext _projectContext;
        public UnitOfWork()
        {
            _projectContext = new Prn221projectContext();
        }

        private GenericDAO<Account> accountDAO;
        public GenericDAO<Account> AccountDAO
        {
            get
            {
                if (accountDAO == null)
                {
                    accountDAO = new AccountDAO();
                }

                return accountDAO;
            }
        }

        private GenericDAO<Category> categoryDAO;
        public GenericDAO<Category> CategoryDAO
        {
            get
            {
                if (categoryDAO == null)
                {
                    categoryDAO = new CategoryDAO();
                }

                return categoryDAO;
            }
        }

        private GenericDAO<Event> eventDAO;
        public GenericDAO<Event> EventDAO
        {
            get
            {
                if (eventDAO == null)
                {
                    eventDAO = new EventDAO();
                }

                return eventDAO;
            }
        }

        private GenericDAO<Promotion> promotionDAO;
        public GenericDAO<Promotion> PromotionDAO
        {
            get
            {
                if (promotionDAO == null)
                {
                    promotionDAO = new PromotionDAO();
                }

                return promotionDAO;
            }
        }

        private GenericDAO<Role> roleDAO;
        public GenericDAO<Role> RoleDAO
        {
            get
            {
                if (roleDAO == null)
                {
                    roleDAO = new RoleDAO();
                }

                return roleDAO;
            }
        }

        private GenericDAO<SolvedTicket> solvedTicketDAO;
        public GenericDAO<SolvedTicket> SolvedTicketDAO
        {
            get
            {
                if (solvedTicketDAO == null)
                {
                    solvedTicketDAO = new SolvedTicketDAO();
                }

                return solvedTicketDAO;
            }
        }

        private GenericDAO<Ticket> ticketDAO;
        public GenericDAO<Ticket> TicketDAO
        {
            get
            {
                if (ticketDAO == null)
                {
                    ticketDAO = new TicketDAO();
                }

                return ticketDAO;
            }
        }

        private GenericDAO<Transaction> transactionDAO;
        public GenericDAO<Transaction> TransactionDAO
        {
            get
            {
                if (transactionDAO == null)
                {
                    transactionDAO = new TransactionDAO();
                }

                return transactionDAO;
            }
        }

        private GenericDAO<TransactionHistory> transactionHistoryDAO;
        public GenericDAO<TransactionHistory> TransactionHistoryDAO
        {
            get
            {
                if (transactionHistoryDAO == null)
                {
                    transactionHistoryDAO = new TransactionHistoryDAO();
                }

                return transactionHistoryDAO;
            }
        }

        private GenericDAO<TransactionTypeDAO> transactionTypeDAO;
        public GenericDAO<TransactionTypeDAO> TransactionTypeDAO
        {
            get
            {
                if (transactionTypeDAO == null)
                {
                    transactionTypeDAO = new TransactionTypeDAO();
                }

                return transactionTypeDAO;
            }
        }

        public void SaveChanges()
        {
            _projectContext.SaveChanges();
        }
    }
}

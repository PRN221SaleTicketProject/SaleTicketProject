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
        public UnitOfWork(Prn221projectContext context)
        {
            _projectContext = context;
        }

        private GenericDAO<Account> accountDAO;
        public GenericDAO<Account> AccountDAO
        {
            get
            {
                if (accountDAO == null)
                {
                    accountDAO = new AccountDAO(_projectContext);
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
                    categoryDAO = new CategoryDAO(_projectContext);
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
                    eventDAO = new EventDAO(_projectContext);
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
                    promotionDAO = new PromotionDAO(_projectContext);
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
                    roleDAO = new RoleDAO(_projectContext);
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
                    solvedTicketDAO = new SolvedTicketDAO(_projectContext);
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
                    ticketDAO = new TicketDAO(_projectContext);
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
                    transactionDAO = new TransactionDAO(_projectContext);
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
                    transactionHistoryDAO = new TransactionHistoryDAO(_projectContext);
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
                    transactionTypeDAO = new TransactionTypeDAO(_projectContext);
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

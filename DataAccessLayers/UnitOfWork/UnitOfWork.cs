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

        private AccountDAO accountDAO;
        public AccountDAO AccountDAO
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

        private CategoryDAO categoryDAO;
        public CategoryDAO CategoryDAO
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

        private EventDAO eventDAO;
        public EventDAO EventDAO
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

        private PromotionDAO promotionDAO;
        public PromotionDAO PromotionDAO
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

        private RoleDAO roleDAO;
        public RoleDAO RoleDAO
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

        private SolvedTicketDAO solvedTicketDAO;
        public SolvedTicketDAO SolvedTicketDAO
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

        private TicketDAO ticketDAO;
        public TicketDAO TicketDAO
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

        private TransactionDAO transactionDAO;
        public TransactionDAO TransactionDAO
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

        private TransactionHistoryDAO transactionHistoryDAO;
        public TransactionHistoryDAO TransactionHistoryDAO
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

        private TransactionTypeDAO transactionTypeDAO;
        public TransactionTypeDAO TransactionTypeDAO
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

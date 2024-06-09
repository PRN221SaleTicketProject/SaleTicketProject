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
    public class TransactionHIstoryRepository : GenericRepository<TransactionHistory>, ITransactionHistoryRepository
    {
        private readonly GenericDAO<TransactionHistory> _transactionHistoryDAO;

        public TransactionHIstoryRepository(GenericDAO<TransactionHistory> transactionHistoryDAO) : base(transactionHistoryDAO)
        {
            _transactionHistoryDAO = transactionHistoryDAO;
        }
    }
}

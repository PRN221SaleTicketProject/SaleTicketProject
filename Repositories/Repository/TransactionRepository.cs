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
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly GenericDAO<Transaction> _transactionDAO;

        public TransactionRepository(GenericDAO<Transaction> transactionDAO) : base(transactionDAO)
        {
            _transactionDAO = transactionDAO;
        }
    }
}

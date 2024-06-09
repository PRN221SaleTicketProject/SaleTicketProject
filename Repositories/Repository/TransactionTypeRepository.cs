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
    public class TransactionTypeRepository : GenericRepository<TransactionType>, ITransactionTypeRepository
    {
        private readonly GenericDAO<TransactionType> _transactionTypeDAO;

        public TransactionTypeRepository(GenericDAO<TransactionType> transactionTypeDAO) : base(transactionTypeDAO)
        {
            _transactionTypeDAO = transactionTypeDAO;
        }
    }
}

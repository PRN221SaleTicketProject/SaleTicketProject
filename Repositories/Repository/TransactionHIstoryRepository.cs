using BusinessObjects;
using BusinessObjects.Enum;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;

        public TransactionHIstoryRepository(GenericDAO<TransactionHistory> transactionHistoryDAO, IUnitOfWork unitOfWork) : base(transactionHistoryDAO)
        {
            _transactionHistoryDAO = transactionHistoryDAO;
            _unitOfWork = unitOfWork;
        }

        public List<TransactionHistory> GetTransactionHistoryByAccountId(int accountId)
        {
            return _unitOfWork.TransactionHistoryDAO.Find(a => a.Transaction!.SolvedTicket!.AccountId == accountId).ToList();
        }

        public List<TransactionHistoryDto> GetAllTransactionHistoryByAccountId(int accountId)
        {
            return _unitOfWork.TransactionHistoryDAO.GetAllTransactionHistoryByAccountId(accountId);
        }
    }
}

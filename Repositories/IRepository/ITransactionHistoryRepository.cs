using BusinessObjects;
using BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ITransactionHistoryRepository : IGenericRepository<TransactionHistory>
    {
        List<TransactionHistory> GetTransactionHistoryByAccountId(int accountId);
        List<TransactionHistoryDto> GetAllTransactionHistoryByAccountId(int accountId);
    }
}

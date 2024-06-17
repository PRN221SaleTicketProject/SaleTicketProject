using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        List<Account> GetAllName();
        void MinusDebt(int? quantity, int? prize, double? discount, Account account);
        Account? GetSystemAccountByEmailAndPassword(string email, string password);
    }
}

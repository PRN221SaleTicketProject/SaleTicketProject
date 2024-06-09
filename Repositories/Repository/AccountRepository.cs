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
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly GenericDAO<Account> _accountDAO;

        public AccountRepository(GenericDAO<Account> accountDAO) : base(accountDAO)
        {
            _accountDAO = accountDAO;
        }
    }
}

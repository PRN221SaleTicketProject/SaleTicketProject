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
        private readonly AccountDAO _accountDAOHigher;//example nang cao code

        public AccountRepository(GenericDAO<Account> accountDAO, AccountDAO accountDAOHigher) : base(accountDAO)
        {
            _accountDAO = accountDAO;
            _accountDAOHigher = accountDAOHigher;
        }

        public List<Account> GetAllName()//vi du service nang cao
        {
            return _accountDAOHigher.GetAllName();
        }
    }
}

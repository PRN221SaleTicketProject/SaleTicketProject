using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly GenericDAO<Account> _accountDAO;
        private readonly AccountDAO _accountDAOHigher;//example nang cao code
        private readonly IUnitOfWork _unitOfWork;

        public AccountRepository(GenericDAO<Account> accountDAO, AccountDAO accountDAOHigher, IUnitOfWork unitOfWork) : base(accountDAO)
        {
            _accountDAO = accountDAO;
            _accountDAOHigher = accountDAOHigher;
            _unitOfWork = unitOfWork;
        }

        public List<Account> GetAllName()//vi du service nang cao
        {
            return _accountDAOHigher.GetAllName();
        }

        public void MinusDebt(int? quantity, int? prize, double? discount, Account account)
        {
            int? accountPay = 0;
            if (quantity < 10) accountPay = int.Parse(account.Address!) - prize * quantity;
            else accountPay = int.Parse(account.Address!) - (int?)((double)prize! * (double)quantity - (double)prize * (double)quantity * discount);

            if (accountPay >= 0)
            {
                account.Address = (accountPay).ToString();
                _unitOfWork.AccountDAO.Update(account);
                _unitOfWork.SaveChanges();
            }
        }

        public Account? GetSystemAccountByEmailAndPassword(string email, string password)
        {
            return _unitOfWork.AccountDAO.GetSystemAccountByAccountEmailAndPassword(email, password);
        }
    }
}

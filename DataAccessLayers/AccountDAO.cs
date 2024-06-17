using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class AccountDAO : GenericDAO<Account>
    {
        // co cach code gọi DAO của rieng model moi model
        // cach: chuyen private thanh public của context o genericDAO và dung _context cua genericDAO

        public List<Account> GetAllName()
        {
            return _context.Set<Account>().ToList();
        }

        public Account? GetSystemAccountByAccountEmailAndPassword(string accountEmail, string password)
        {
            return _context.Accounts.SingleOrDefault(m => m.Email == accountEmail && m.Password == password);
        }
    }
}

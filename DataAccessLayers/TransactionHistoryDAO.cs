using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class TransactionHistoryDAO(Prn221projectContext context) : GenericDAO<TransactionHistory>(context)
    {
    }
}

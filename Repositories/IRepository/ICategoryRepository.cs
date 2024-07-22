using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public Category? getByCateName(string name);
    }
}

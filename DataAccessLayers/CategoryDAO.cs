using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class CategoryDAO : GenericDAO<Category>
    {
        public Category? getByCateName(string name)
        {
            return _context.Categories.FirstOrDefault(a => a.Type == name);
        }
    }
}

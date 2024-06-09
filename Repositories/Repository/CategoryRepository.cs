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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly GenericDAO<Category> _categoryDAO;

        public CategoryRepository(GenericDAO<Category> categoryDAO) : base(categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }
    }
}

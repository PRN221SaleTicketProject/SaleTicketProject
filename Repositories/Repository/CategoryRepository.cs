using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUnitOfWork _unitOfWork;
        public CategoryRepository(GenericDAO<Category> categoryDAO, IUnitOfWork unitOfWork) : base(categoryDAO)
        {
            _categoryDAO = categoryDAO;
            _unitOfWork = unitOfWork;
        }
        public Category? getByCateName(string name)
        {
            return _unitOfWork.CategoryDAO.getByCateName(name);
        }
    }
}
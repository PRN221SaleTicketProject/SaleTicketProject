using DataAccessLayers;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly GenericDAO<T> _genericDAO;

        public GenericRepository(GenericDAO<T> genericDAO)
        {
            _genericDAO = genericDAO;
        }

        public T? GetById(int id) { return _genericDAO.GetById(id); }
        public IEnumerable<T> GetAll() { return _genericDAO.GetAll(); }
        public T Add(T entity) { return _genericDAO.Add(entity); }
        public void Update(T entity) { _genericDAO.Update(entity); }
        public void Delete(int id) { _genericDAO.Delete(id); }
    }
}

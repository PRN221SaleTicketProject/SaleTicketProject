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
        public T? FindOne(Expression<Func<T, bool>> Object) { return _genericDAO.FindOne(Object); }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression) { return _genericDAO.Find(expression); }
        public T Add(T entity) { return _genericDAO.Add(entity); }
        public IEnumerable<T> AddRange(IEnumerable<T> entities) { return _genericDAO.AddRange(entities); }
        public void DeleteRange(IEnumerable<T> entities) { _genericDAO.DeleteRange(entities); }
        public void UpdateRange(IEnumerable<T> entities) { _genericDAO.UpdateRange(entities); }
        public void Update(T entity) { _genericDAO.Update(entity); }
        public void Delete(int id) { _genericDAO.Delete(id); }
        public bool Exists(int id) { return _genericDAO.Exists(id); }
        public T? FindOneWithNoTracking(Expression<Func<T, bool>> expression) { return _genericDAO.FindOneWithNoTracking(expression); }
        public bool Any(Expression<Func<T, bool>> expression) { return _genericDAO.Any(expression); }
    }
}

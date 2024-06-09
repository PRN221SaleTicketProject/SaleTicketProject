using DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        T? GetById(int id);
        IEnumerable<T> GetAll();
        T? FindOne(Expression<Func<T, bool>> Object) ;
        IEnumerable<T> Find(Expression<Func<T, bool>> expression) ;
        T Add(T entity) ;
        IEnumerable<T> AddRange(IEnumerable<T> entities) ;
        void DeleteRange(IEnumerable<T> entities);
        void UpdateRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(int id) ;
        bool Exists(int id) ;
        T? FindOneWithNoTracking(Expression<Func<T, bool>> expression) ;
        bool Any(Expression<Func<T, bool>> expression) ; 
    }
}

using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class GenericDAO<T> where T : class
    {
        protected readonly Prn221projectContext _context;
        public GenericDAO()
        {
            _context = new Prn221projectContext();
        }

        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T? FindOne(Expression<Func<T, bool>> expression)
        {
            return  _context.Set<T>().Where(expression).FirstOrDefault();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return  _context.Set<T>().Where(expression).ToList();
        }

        public T Add(T entity)
        {
            _context.AddAsync(entity);
            _context.SaveChanges();
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.AddRange(entities);
            _context.SaveChanges();
            return entities;
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            var enumerable = entities as T[] ?? entities.ToArray();
            if (enumerable.Any())
            {
                _context.Set<T>().RemoveRange(enumerable);
                _context.SaveChanges();
            }
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            var enumerable = entities as T[] ?? entities.ToArray();
            if (enumerable.Any())
            {
                _context.Set<T>().UpdateRange(enumerable);
                _context.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public  bool Exists(int id)
        {
            var entity =  GetById(id);
            return entity != null;
        }

        public T? FindOneWithNoTracking(Expression<Func<T, bool>> expression)
        {
            var dbObject =  _context.Set<T>().Where(expression).AsNoTracking().FirstOrDefault();
            return dbObject;
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }

        public static implicit operator GenericDAO<T>(TransactionTypeDAO v)
        {
            throw new NotImplementedException();
        }
    }
}

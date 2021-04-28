using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace _0_Framework.Infrastructure
{
    public class RepositoryBase<TKey,T>:IRepository<TKey,T> where T:class
    {
        private readonly DbContext _db;

        public RepositoryBase(DbContext db)
        {
            _db = db;
        }
        public T Get(TKey id)
        {
            return _db.Find<T>(id);
        }

        public List<T> Get()
        {
            return _db.Set<T>().ToList();
        }

        public void Create(T entity)
        {
            _db.Add(entity);
        }

        public void SaveChanges()
        {
            _db.SaveChanges(); 
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return _db.Set<T>().Any(expression);
        }
    }
}

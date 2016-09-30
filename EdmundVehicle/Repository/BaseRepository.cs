using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace EdmundVehicle.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        protected readonly DbContext dbContext;
        protected readonly DbSet<T> dbSet;

        public BaseRepository(DbContext context)
        {
            dbContext = context;
            dbSet = dbContext.Set<T>();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public IQueryable<T> GetAll()
        {
            return dbSet.AsQueryable();
        }

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public T Insert(T o)
        {
            var newEntity = dbSet.Add(o);
            return newEntity;
        }

        public IQueryable<T> InsertMulti(IEnumerable<T> collection)
        {
            var newEntites = dbSet.AddRange(collection).AsQueryable();
            return newEntites;
        }

        public T Update(T o)
        {
            throw new NotImplementedException();
        }

        public void Delete(T o)
        {
            dbSet.Remove(o);
        }

        public void DeleteMulti(IEnumerable<T> collection)
        {
            dbSet.RemoveRange(collection);
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
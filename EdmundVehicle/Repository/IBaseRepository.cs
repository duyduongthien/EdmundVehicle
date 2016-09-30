using System;
using System.Collections.Generic;
using System.Linq;

namespace EdmundVehicle.Repository
{
    public interface IBaseRepository<T> : IDisposable
    {
        IQueryable<T> GetAll();
        T GetById(object id);
        T Insert(T o);
        IQueryable<T> InsertMulti(IEnumerable<T> collection);
        T Update(T o);
        void Delete(T o);
        void DeleteMulti(IEnumerable<T> collection);
        void Save();
    }
}
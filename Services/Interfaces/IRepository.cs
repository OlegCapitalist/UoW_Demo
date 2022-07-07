using System.Collections.Generic;
using Infrastructure;

namespace Services.Interfaces
{
    public interface IRepository<T> where T : class
    {
        UniversityDbContext Context { get; }
        IUnitOfWork<UniversityDbContext> UnitOfWork { get; }

        delegate void DataBaseAction(T item);

        List<T> GetList();

        List<T> GetListByParrent(int parrentId);

        T GetById(int id);

        void Add(T item);

        void Edit(T item);

        void Delete(T item);

        void WriteIntoDataBase(T item, DataBaseAction dataBaseAction);

    }
}

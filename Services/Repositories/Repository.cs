using Services.Interfaces;
using Infrastructure;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Services.Repositories
{
    public abstract class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        protected DbSet<T> _table;
        private string _errorMessage = string.Empty;
        private bool _isDisposed;

        public Repository(IUnitOfWork<UniversityDbContext> unitOfWork)
            : this(unitOfWork.Context)
        {
            UnitOfWork = unitOfWork;
        }

        public Repository(UniversityDbContext context)
        {
            _isDisposed = false;
            Context = context;
        }

        public UniversityDbContext Context { get; }
        public IUnitOfWork<UniversityDbContext> UnitOfWork { get; }

        public event EventHandler<TransactionEventArgs> beforeSave;
        public event EventHandler<TransactionEventArgs> onSave;
        public event EventHandler<TransactionEventArgs> beforeDelete;

        protected virtual DbSet<T> Table
        {
            get { return _table ?? (_table = Context.Set<T>()); }
        }

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            Table.Add(item);
        }

        public void Delete(T item)
        {

            var args = new TransactionEventArgs();
            beforeDelete?.Invoke(item, args);

            if (args.Cancel)
                throw new Exception("Item cannot be deleted!");
            else
            {
                if (item == null)
                    throw new ArgumentNullException("entity");
                Table.Remove(item);
                    
            }
        }

        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
            _isDisposed = true;
        }

        public void Edit(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            SetEntryModified(item);
        }

        public T GetById(int id)
        {
            return Table.Find(id);
        }

        public virtual List<T> GetList()
        {
            return Table.ToList();
        }

        public abstract List<T> GetListByParrent(int parrentId);

        public virtual void SetEntryModified(T item)
        {
            Context.Entry(item).State = EntityState.Modified;
        }

        public void WriteIntoDataBase(T item, IRepository<T>.DataBaseAction dataBaseAction)
        {
            if (dataBaseAction != null)
                try
                {
                    UnitOfWork.CreateTransaction();                   
                    dataBaseAction(item);
                    UnitOfWork.Save();
                    UnitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    UnitOfWork.Rollback();
                }
        }
    }
}

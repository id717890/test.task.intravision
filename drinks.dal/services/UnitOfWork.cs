using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drinks.dal.services
{
    using System;
    using System.Collections.Generic;
    using drinks.dal.@interface.services;
    using drinks.domain.@interface.entities;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DrinkContext _context;
        private bool _disposed;
        private Dictionary<string, object> _repositories;

        public UnitOfWork(DrinkContext context)
        {
            _context = context;
        }

        public UnitOfWork()
        {
            _context = new DrinkContext();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public IRepository<T> Repository<T>() where T : Entity
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (Repository<T>)_repositories[type];
        }
    }
}

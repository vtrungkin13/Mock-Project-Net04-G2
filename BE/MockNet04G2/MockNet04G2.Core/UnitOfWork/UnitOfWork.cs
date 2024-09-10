using MockNet04G2.Core.Data;
using MockNet04G2.Core.Repositories;
using MockNet04G2.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MockDbContext _context;


        public UnitOfWork(MockDbContext context)
        {
            _context = context;
        }
        public MockDbContext Context => _context;

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            return new GenericRepository<T>(Context);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MockNet04G2.Core.Data;
using MockNet04G2.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly MockDbContext _context;
        protected readonly DbSet<T> _entities;

        public GenericRepository(MockDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
        }

        public void Delete(int id)
        {
            var entity = _entities.Find(id);
            _entities.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.RemoveRange(entities);
        }

        public virtual T Find(int id)
        {
            return _entities.Find(id);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        IEnumerable<T> IGenericRepository<T>.GetAll()
        {
            return _entities.ToList();
        }
    }
}

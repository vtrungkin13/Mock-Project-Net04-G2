using Microsoft.EntityFrameworkCore;
using MockNet04G2.Business.Services.Interfaces;
using MockNet04G2.Core.Repositories.Interfaces;
using MockNet04G2.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is null!");
            }
            _unitOfWork.GenericRepository<T>().Add(entity);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> AddRange(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("Entity is null!");
            }
            _unitOfWork.GenericRepository<T>().AddRange(entities);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is null!");
            }
            _unitOfWork.GenericRepository<T>().Delete(entity);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {

            _unitOfWork.GenericRepository<T>().Delete(id);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> DeleteRange(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("Entity is null!");
            }
            _unitOfWork.GenericRepository<T>().DeleteRange(entities);
            return await _unitOfWork.SaveChangesAsync();
        }

        public T Find(int id)
        {
            return _unitOfWork.GenericRepository<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _unitOfWork.GenericRepository<T>().GetAll();
        }

        public async Task<int> Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is null!");
            }
            _unitOfWork.GenericRepository<T>().Update(entity);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}

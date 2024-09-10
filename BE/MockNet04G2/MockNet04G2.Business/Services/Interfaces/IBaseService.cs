using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Find(int id);
        Task<int> Add(T entity);
        Task<int> AddRange(IEnumerable<T> entities);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
        Task<int> Delete(int id);
        Task<int> DeleteRange(IEnumerable<T> entities);
    }
}

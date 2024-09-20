using MockNet04G2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Core.Repositories.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<Organization> FindByPhoneOrNameAsync(string phone, string name);
        Task<List<Organization>> FindByIdAsync(List<int> ids);
        IQueryable<Organization> GetAll();

    }
}

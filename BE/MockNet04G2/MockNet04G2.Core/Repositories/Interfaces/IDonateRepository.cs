using MockNet04G2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Core.Repositories.Interfaces
{
    public interface IDonateRepository : IGenericRepository<Donate>
    {
        Task<decimal> GetTotalDonateAmount(int id);
        Task<List<Donate>> GetDonationHistory(int uid);
    }

}

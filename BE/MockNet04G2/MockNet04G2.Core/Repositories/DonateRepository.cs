using MockNet04G2.Core.Data;
using MockNet04G2.Core.Models;
using MockNet04G2.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Core.Repositories
{
    public class DonateRepository : GenericRepository<Donate>, IDonateRepository
    {
        public DonateRepository(MockDbContext context) : base(context)
        {
        }
    }
}

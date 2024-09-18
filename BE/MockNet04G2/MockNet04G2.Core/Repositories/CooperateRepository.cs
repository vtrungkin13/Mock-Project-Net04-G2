using Microsoft.EntityFrameworkCore;
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
    public class CooperateRepository : GenericRepository<Cooperate>, ICooperateRepository
    {
        private readonly MockDbContext _context;

        public CooperateRepository(MockDbContext context) :base(context) 
        {
            _context = context;
        }

        public async Task<List<Cooperate>> FindByCampaignIdAsync(int campaignId)
        {
            return await _context.Cooperates
                    .Where(c => c.CampaignId == campaignId)
                    .ToListAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MockNet04G2.Core.Common.Enums;
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
    public class CampaignRepository : GenericRepository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(MockDbContext context) : base(context) { }

        public async Task<List<Campaign>> FilterCampaignsByStatusAsync(StatusEnum status)
        {
           var campaigns = await _entities
                .Include(c => c.Donations)
                .Include(c => c.Cooperations).Where(c=>c.Status == status).ToListAsync();
            return campaigns;
        }

        public async Task<List<Campaign>> GetAllCampaignsAsync()
        {
            var campagins = await _entities
                .Include(c=>c.Donations)
                .Include(c=>c.Cooperations).ToListAsync();
            return campagins;
        }

        public async Task<Campaign> GetCampaignByIdAsync(int id)
        {
            var campaign = await _entities.FindAsync(id);
            return campaign;
        }
    }
}

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

        public async Task<List<Campaign>> FilterCampaignsByStatusAsync(StatusEnum status, int page, int pageSize)
        {
            if (page < 1) page = 1; // Ensure the page number is at least 1
            if (pageSize < 1) pageSize = 9; // Ensure the page size is at least 1

            var campaigns = await _entities
                .Include(c => c.Donations)
                    .ThenInclude(u => u.User)
                .Include(c => c.Cooperations)
                    .ThenInclude(o => o.Organization)
                .Where(c => c.Status == status)
                .OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * pageSize) // Skip items for previous pages
                .Take(pageSize) // Take the items for the current page
                .ToListAsync();

            return campaigns;
        }


        public async Task<List<Campaign>> GetAllCampaignsAsync()
        {
            var campagins = await _entities
                .Include(c => c.Donations)
                 .ThenInclude(u => u.User)
                 .Include(c => c.Cooperations)
                 .ThenInclude(o => o.Organization).ToListAsync();
            return campagins;
        }

        public async Task<Campaign> GetCampaignByIdAsync(int id)
        {
            var campaign = await _entities
                 .Include(c => c.Donations)
                 .ThenInclude(u => u.User)
                 .Include(c => c.Cooperations)
                 .ThenInclude(o => o.Organization)
                 .FirstOrDefaultAsync(c => c.Id == id);

            return campaign;
        }

        public async Task<List<Campaign>> CampaignPagingAsync(int page, int pageSize)
        {
            // Validate the page number and page size
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 9;

            // Apply pagination logic using Skip and Take
            var campaigns = await _entities
                .Include(c => c.Donations)
                .ThenInclude(d => d.User)
                .Include(c => c.Cooperations)
                .ThenInclude(co => co.Organization)
                .OrderByDescending(c => c.CreatedAt) // sắp xếp theo createdAt
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return campaigns;
        }

        public async Task<int> TotalCampaignsCountAsync()
        {
            return await _entities.CountAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Data;
using MockNet04G2.Core.Models;
using MockNet04G2.Core.Repositories.Interfaces;
using System.Web; // For HttpUtility.UrlDecode
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
        public IQueryable<Campaign> GetAll()
        {
            return _context.Campaigns.AsQueryable();
        }

        public IQueryable<Cooperate> GetCooperations()
        {
            return _context.Cooperates.AsQueryable();
        }

        public async Task<List<Campaign>> SearchCampaignsAsync(string campaignCode, string organizationPhone)
        {
            var query = _context.Campaigns.AsQueryable();

            if (!string.IsNullOrEmpty(campaignCode))
            {
                query = query.Where(c => c.Code == campaignCode);
            }

            if (!string.IsNullOrEmpty(organizationPhone))
            {
                query = query.Where(c => c.Cooperations.Any(co => co.Organization.Phone == organizationPhone));
            }

            return await query.ToListAsync();
        }
        public async Task<int> TotalCampaignsCountAfterFilterAsync(StatusEnum status)
        {
            return await _entities.Where(c => c.Status == status).CountAsync();
        }

        public async Task<List<Campaign>> GetHomePageCampaignAsync(int pageSize, int page, string code, string phone, StatusEnum? status)
        {
            // Validate page number and page size
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 9;

            // Decode the code string if it's URL-encoded
            if (!string.IsNullOrEmpty(code))
            {
                code = HttpUtility.UrlDecode(code).Replace(" ", "").ToLower();
            }

            // Start building the query for campaigns
            var query = _entities
                .Include(c => c.Donations)
                .ThenInclude(d => d.User)
                .Include(c => c.Cooperations)
                .ThenInclude(co => co.Organization)
                .AsQueryable();

            // Apply filtering conditions if provided
            // Filter by organization phone (from Cooperations)
            if (!string.IsNullOrEmpty(phone))
            {
                // Remove spaces from both the database value and the input phone number
                query = query.Where(c => c.Cooperations
                    .Any(co => co.Organization.Phone.Replace(" ", "").ToLower()
                    .Contains(phone.Replace(" ", "").ToLower())));
            }

            // Filter by status if provided
            if (status.HasValue)
            {
                query = query.Where(c => c.Status == status);
            }

            // Fetch filtered campaigns from the database
            var campaigns = await query.ToListAsync();

            // Filter by code in memory
            if (!string.IsNullOrEmpty(code))
            {
                // Convert all relevant properties to strings for comparison
                campaigns = campaigns
                    .Where(c => (c.CreatedAt.ToString() + c.Id.ToString()).Replace(" ", "").ToLower().Contains(code))
                    .ToList();
            }

            // Apply pagination after filtering
            campaigns = campaigns
                .OrderByDescending(c => c.CreatedAt) // Sorting by CreatedAt
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return campaigns;
        }

        public async Task<int> GetHomePageCampaignCountAsync(string code, string phone, StatusEnum? status)
        {
            // Start building the query for campaigns
            var query = _entities
                .Include(c => c.Cooperations)
                .ThenInclude(co => co.Organization)
                .AsQueryable();

            // Decode the code string if it's URL-encoded
            if (!string.IsNullOrEmpty(code))
            {
                code = HttpUtility.UrlDecode(code).Replace(" ", "").ToLower();
            }

            // Apply filtering conditions if provided

            // Filter by organization phone (from Cooperations)
            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(c => c.Cooperations
                    .Any(co => co.Organization.Phone.Trim().ToLower().Contains(phone.Trim().ToLower())));
            }

            // Filter by status if provided
            if (status.HasValue)
            {
                query = query.Where(c => c.Status == status);
            }

            // Fetch data from the database and then filter by Code in memory
            var campaigns = await query.ToListAsync();

            // Filter by code in memory
            if (!string.IsNullOrEmpty(code))
            {
                // Convert all relevant properties to strings for comparison
                campaigns = campaigns
                    .Where(c => (c.CreatedAt.ToString() + c.Id.ToString()).Replace(" ", "").ToLower().Contains(code))
                    .ToList();
            }

            // Return the count of filtered campaigns
            return campaigns.Count;
        }
    }
}

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
    public class DonateRepository : GenericRepository<Donate>, IDonateRepository
    {
        private readonly MockDbContext _context;
        public DonateRepository(MockDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<decimal> GetTotalDonateAmount(int id)
        {
            var total = await _context.Campaigns.Where((Campaign c) => c.Id == id)
                                                .SelectMany(c => c.Donations)
                                                .SumAsync(d => d.Amount);
            return total;
        }

        public async Task<List<Donate>> GetDonationHistory(int uid)
        {
            var list = await _context.Donates.Where(d => d.UserId == uid)
                .Include(c => c.Campaign).ToListAsync();
            return list;
        }
    }
}

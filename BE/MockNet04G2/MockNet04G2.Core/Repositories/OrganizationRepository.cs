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
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly MockDbContext _context;

        public OrganizationRepository(MockDbContext context)
        {
            _context = context;
        }

        public async Task<List<Organization>> FindByIdAsync(List<int> ids)
        {
            return await _context.Organizations
                   .Where(o => ids.Contains(o.Id))
                   .ToListAsync();
        }

        public async Task<Organization> FindByPhoneOrNameAsync(string phone, string name)
        {
            return await _context.Organizations.FirstOrDefaultAsync(o => o.Phone == phone || o.Name == name);
        }

        public IQueryable<Organization> GetAll()
        {
            return _context.Organizations.AsQueryable();
        }

        public async Task<List<Organization>> GetAllOrganizationAsync()
        {
            var organizations = await _context.Organizations.ToListAsync();
            return organizations;
        }
    }
}

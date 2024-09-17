using Microsoft.EntityFrameworkCore;
using MockNet04G2.Core.Data;
using MockNet04G2.Core.Models;
using MockNet04G2.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Core.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MockDbContext context) : base(context) { }

        public async Task<User> FindUserByEmailorPhone(string emailOrPhone)
        {
            var user = await _entities.SingleOrDefaultAsync(user => user.Email == emailOrPhone || user.Phone == emailOrPhone);
            return user;
        }

        public async Task<User> FindUserByPhoneAsync(string phone)
        {
            var user = await _entities.SingleOrDefaultAsync(x => x.Phone == phone);
            return user;
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            var user = await _entities.SingleOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            var users = await _entities.ToListAsync();
            return users;
        }
    }
}

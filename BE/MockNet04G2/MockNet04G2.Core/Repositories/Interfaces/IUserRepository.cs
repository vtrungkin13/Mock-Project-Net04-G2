﻿using MockNet04G2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Core.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> FindUserByEmailorPhone(string emailOrPhone);
        Task<User> FindUserByPhoneAsync(string phone);
        Task<User> FindUserByEmailAsync(string email);
        Task<List<User>> FindUserByNameAsync(string name);
        Task<User> FindUserByIdAsync(int id);
        Task ChangeUserRoleAsync(User user);
        Task<List<User>> GetAllUserAsync();
        Task<List<User>> UserPagingAsync(int page, int pageSize);
        Task<int> TotalUserCountAsync();
        Task<List<User>> FilterUser(int pageSize, int page, string name);
        Task<int> FilterUserCount(string name);
    }
}

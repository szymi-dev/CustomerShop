using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreContext _context;
        public UserRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.Where(u => u.UserName == username)
                .Include(x => x.LikedProducts)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users
            .Include(x => x.Products)
            .ToListAsync();
        }
    }
}
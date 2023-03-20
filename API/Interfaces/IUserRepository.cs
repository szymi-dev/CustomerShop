using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(int id);
        Task<User> GetUserByUsername(string username);
        Task<List<User>> GetUsers();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApiExample.Models;

namespace AspNetCoreApiExample.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private MySqlContext _context;

        public UserRepository(MySqlContext context)
        {
            _context = context;
        }

        public User FindByLogin(string user)
        {
            return _context.Users.Where(s => s.Login == user).FirstOrDefault();
        }
    }
}

using AspNetCoreApiExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApiExample.Repository
{
    public interface IUserRepository
    {
        User FindByLogin(string user);
    }
}

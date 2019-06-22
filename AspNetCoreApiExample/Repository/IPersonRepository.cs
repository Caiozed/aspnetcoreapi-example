using AspNetCoreApiExample.Data.VO;
using AspNetCoreApiExample.Models;
using AspNetCoreApiExample.Repository.Generic;
using System.Collections.Generic;

namespace AspNetCoreApiExample.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        List<Person> FindByName(string firstName, string lastName);
    }
}

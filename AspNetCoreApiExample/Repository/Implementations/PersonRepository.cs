using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApiExample.Data.VO;
using AspNetCoreApiExample.Models;

namespace AspNetCoreApiExample.Repository.Implementations
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySqlContext context) : base (context)
        {
        }
     
        public List<Person> FindByName(string firstName, string lastName)
        {
            return _context.Persons.Where(w => w.FirstName == firstName && w.LastName == lastName).ToList();
        }
    }
}

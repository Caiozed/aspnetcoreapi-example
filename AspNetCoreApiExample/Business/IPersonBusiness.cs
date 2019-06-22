using AspNetCoreApiExample.Data.VO;
using AspNetCoreApiExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApiExample.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
        List<PersonVO> FindByName(string firstName, string lastName);
        List<PersonVO> FindWithPagedSearch(int pageSize, int page);
        PersonVO Update(PersonVO person);
        bool Delete(long id);


    }
}

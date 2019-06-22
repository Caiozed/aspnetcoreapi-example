using AspNetCoreApiExample.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApiExample.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T obj);
        T FindById(long id);
        List<T> FindAll();
        T Update(T obj);
        bool Delete(long id);
        List<T> FindWithPagedSearch(int pageSize, int page);
    }
}

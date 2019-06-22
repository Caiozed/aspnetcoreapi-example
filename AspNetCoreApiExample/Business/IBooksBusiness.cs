using AspNetCoreApiExample.Data.VO;
using AspNetCoreApiExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApiExample.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO person);
        BookVO FindById(long id);
        List<BookVO> FindAll();
        BookVO Update(BookVO person);
        bool Delete(long id);


    }
}

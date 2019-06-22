using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApiExample.Data.Converters;
using AspNetCoreApiExample.Data.VO;
using AspNetCoreApiExample.Models;
using AspNetCoreApiExample.Repository.Generic;

namespace AspNetCoreApiExample.Business.Implementations
{
    public class BookBusiness : IBookBusiness
    {
        private IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusiness(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Create(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public bool Delete(long id)
        {
            return _repository.Delete(id);
        }

        public List<BookVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public BookVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }
    }
}

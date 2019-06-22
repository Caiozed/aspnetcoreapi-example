using AspNetCoreApiExample.Data.Converter;
using AspNetCoreApiExample.Data.VO;
using AspNetCoreApiExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApiExample.Data.Converters
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {

        public BookVO Parse(Book input)
        {
            if (input == null) return null;
            return new BookVO
            {
                Id = input.Id,
                Author = input.Author,
                LauchDate = input.LauchDate,
                Title = input.Title,
                Price = input.Price
            };
        }

        public Book Parse(BookVO input)
        {
            if (input == null) return null;
            return new Book
            {
                Id = input.Id,
                Author = input.Author,
                LauchDate = input.LauchDate,
                Title = input.Title,
                Price = input.Price
            };
        }


        public List<BookVO> ParseList(List<Book> inputList)
        {
            if (inputList == null) return null;
            return inputList.Select(i => Parse(i)).ToList();
        }

        public List<Book> ParseList(List<BookVO> inputList)
        {
            if (inputList == null) return null;
            return inputList.Select(i => Parse(i)).ToList();
        }

    }
}

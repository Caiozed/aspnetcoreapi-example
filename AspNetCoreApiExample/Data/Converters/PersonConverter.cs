using AspNetCoreApiExample.Data.Converter;
using AspNetCoreApiExample.Data.VO;
using AspNetCoreApiExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApiExample.Data.Converters
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public PersonVO Parse(Person input)
        {
            if (input == null) return null;
            return new PersonVO
            {
                Id = input.Id,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Address = input.Address,
                Gender = input.Address
            };
        }

        public Person Parse(PersonVO input)
        {
            if (input == null) return null;
            return new Person
            {
                Id = input.Id,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Address = input.Address,
                Gender = input.Address
            };
        }


        public List<PersonVO> ParseList(List<Person> inputList)
        {
            if (inputList == null) return null;
            return inputList.Select(i => Parse(i)).ToList();
        }

        public List<Person> ParseList(List<PersonVO> inputList)
        {
            if (inputList == null) return null;
            return inputList.Select(i => Parse(i)).ToList();
        }
    }
}

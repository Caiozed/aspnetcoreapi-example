using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApiExample.Data.Converters;
using AspNetCoreApiExample.Data.VO;
using AspNetCoreApiExample.Models;
using AspNetCoreApiExample.Repository;
using AspNetCoreApiExample.Repository.Generic;

namespace AspNetCoreApiExample.Business.Implementations
{
    public class PersonBusiness : IPersonBusiness
    {
        private IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusiness(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public bool Delete(long id)
        {
            return _repository.Delete(id);
        }

        public List<PersonVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.ParseList(_repository.FindByName(firstName, lastName));
        }

        public List<PersonVO> FindWithPagedSearch(int pageSize, int page)
        {
            pageSize = pageSize == 0 ? 50 : pageSize;
            page = page == 0 ? 1 : page;
            return _converter.ParseList(_repository.FindWithPagedSearch(pageSize, page));
        }
    }
}

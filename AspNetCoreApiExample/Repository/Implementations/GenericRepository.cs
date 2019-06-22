using AspNetCoreApiExample.Models;
using AspNetCoreApiExample.Models.Base;
using AspNetCoreApiExample.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApiExample.Repository.Implementations
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        protected MySqlContext _context;
        private DbSet<T> dataset;

        public GenericRepository(MySqlContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }

        public T Create(T obj)
        {
            try
            {
                dataset.Add(obj);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            return obj;
        }

        public bool Delete(long id)
        {
            var person = dataset.Where(a => a.Id == id).FirstOrDefault();
            if (person == null) return false;

            dataset.Remove(person);
            _context.SaveChanges();
            return true;
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindById(long id)
        {
            return dataset.Where(s => s.Id == id).FirstOrDefault();
        }

        public List<T> FindWithPagedSearch(int pageSize, int page)
        {
            return dataset.Skip(pageSize * page - 1).Take(pageSize).ToList();
        }

        public T Update(T obj)
        {
            try
            {
                dataset.Update(obj);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            return obj;
        }
    }
}

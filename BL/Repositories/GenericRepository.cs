using BL.Interfaces;
using DL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly RealStateDbContext _dbContext;

        public GenericRepository(RealStateDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(T entity)
        {
            _dbContext.Add(entity);
         
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
            
        }

        public IEnumerable<T> GetAll()
        {
          return _dbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id); 
        }

        public void Update(T entity)
        {
           _dbContext.Update(entity);
            
        }
    }
}

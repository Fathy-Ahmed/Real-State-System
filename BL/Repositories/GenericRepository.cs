using BL.Interfaces;
using DL.Context;
using Microsoft.EntityFrameworkCore;
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


        public async Task Add(T entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
            
        }

        public async Task<IEnumerable<T>> GetAll()
        {
          return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id); 
        }

        public void Update(T entity)
        {
           _dbContext.Update(entity);
            
        }
    }
}

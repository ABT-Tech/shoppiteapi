using Shoppite.Core.Repositories.Base;
using Shoppite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppite.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ShoppiteContext _shoppiteContext;

        public Repository(ShoppiteContext employeeContext)
        {
            _shoppiteContext = employeeContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _shoppiteContext.Set<T>().AddAsync(entity);
            await _shoppiteContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _shoppiteContext.Set<T>().Remove(entity);
            await _shoppiteContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _shoppiteContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _shoppiteContext.Set<T>().FindAsync(id);
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

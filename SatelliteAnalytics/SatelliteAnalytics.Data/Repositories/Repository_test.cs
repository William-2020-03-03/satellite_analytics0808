using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteAnalytics.Data.Repositories
{
    public class Repository_test<T> : IRepository_test<T> where T : class
    {
        protected readonly SatelliteAnalyticsDBContext _context;

        protected readonly DbSet<T> _dbSet;

        public Repository_test(SatelliteAnalyticsDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        //public async Task<T> GetByIdAsync(Guid id)
        //{
        //    return await _dbSet.FindAsync(id);
        //}

        //public async Task<IEnumerable<T>> GetAllAsync()
        //{
        //    return await _dbSet.ToListAsync();
        //}

        //public async Task AddAsync(T entity)
        //{
        //    await _dbSet.AddAsync(entity);
        //}

        //public void Update(T entity)
        //{
        //    _dbSet.Update(entity);
        //}

        //public void Remove(T entity)
        //{
        //    _dbSet.Remove(entity);
        //}

        //public async Task SaveChangesAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}
    }

}

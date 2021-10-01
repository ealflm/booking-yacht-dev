using BookingYacht.Data.Context;
using BookingYacht.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BookingYacht.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(BookingYachtContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
        }

        public DbSet<TEntity> Query()
        {
            return _dbSet;
        }

        public async Task<TEntity> GetById(Guid id)
        {
            var data = await _dbSet.FindAsync(id);
            return data;
        }

        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public async Task Remove(Guid id)
        {
            var entity = await GetById(id);
            _dbSet.Remove(entity);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class Repository<TEntity>:IRepository<TEntity> where TEntity:Entity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbSet<TEntity> dbSet, DbContext context)
        {
            _dbSet = dbSet;
            _context = context;
        }

        public int Create(TEntity entity)
        {
            var entityEntry = _dbSet.Add(entity);
            _context.SaveChanges();
            return entityEntry.Entity.Id;
        }

        public TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IEnumerable<TEntity> Filter(Func<TEntity, bool> filter)
        {
            return _dbSet.Where(filter);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            Remove(FindById(id));
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public DbSet<TEntity> DbSet => _dbSet;
    }
}

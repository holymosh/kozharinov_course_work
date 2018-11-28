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

        public Repository(CommonContext commonContext)
        {
            DbSet = commonContext.Set<TEntity>();
            _context = commonContext;
        }

        public int Create(TEntity entity)
        {
            var entityEntry = DbSet.Add(entity);
            _context.SaveChanges();
            return entityEntry.Entity.Id;
        }

        public TEntity FindById(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public IEnumerable<TEntity> Filter(Func<TEntity, bool> filter)
        {
            return DbSet.Where(filter);
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
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

        public DbSet<TEntity> DbSet { get; }
    }
}

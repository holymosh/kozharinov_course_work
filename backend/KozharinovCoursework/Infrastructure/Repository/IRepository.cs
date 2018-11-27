using System;
using System.Collections.Generic;
using System.Text;
using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity: Entity
    {
        int Create(TEntity entity);
        TEntity FindById(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Filter(Func<TEntity, bool> filter);
        void Remove(TEntity entity);
        void Remove(int id);
        void Update(TEntity entity);
        DbSet<TEntity> DbSet { get; }
        
    }
}

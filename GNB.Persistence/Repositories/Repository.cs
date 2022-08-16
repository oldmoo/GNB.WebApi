using System.Linq.Expressions;
using GNB.Domain.Entities;
using GNB.Domain.InfrastructureContracts;
using GNB.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GNB.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
     protected readonly AppDbContext Context;
     private readonly DbSet<TEntity> _dbSet;
     public Repository(AppDbContext context)
     {
          Context = context;
          _dbSet = context.Set<TEntity>();
     }
     
     public async Task<IEnumerable<TEntity>> ListAllAsync()
     {
          return await _dbSet.AsNoTracking().ToListAsync();
     }

     public async Task AddRangeAsync(IEnumerable<TEntity> entities)
     {
          if (_dbSet.Any())
          {
               _dbSet.RemoveRange(_dbSet);
          }
          await _dbSet.AddRangeAsync(entities);
     }

     public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate) =>
          await _dbSet.AnyAsync(predicate);
}
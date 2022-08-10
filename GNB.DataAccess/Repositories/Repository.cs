using System.Linq.Expressions;
using GNB.Domain.Entities;
using GNB.Domain.InfrastructureContracts;
using GNB.Infrastructure.Contexts;
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

     public async Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken)
     {
          return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
     }

     public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
     {
          if (_dbSet.Any())
          {
               _dbSet.RemoveRange(_dbSet);
          }
          await _dbSet.AddRangeAsync(entities, cancellationToken);
     }

     public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate) =>
          await _dbSet.AnyAsync(predicate);
}
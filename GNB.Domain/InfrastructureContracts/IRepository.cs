using System.Linq.Expressions;
using GNB.Domain.Entities;

namespace GNB.Domain.InfrastructureContracts;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
     Task<IEnumerable<TEntity>> ListAllAsync(IEnumerable<Expression<Func<TEntity, bool>>>? predicates = null);
     Task<IEnumerable<TEntity>> ListAllAsync(int skip, int limit, IEnumerable<Expression<Func<TEntity, bool>>>? predicates = null);
     Task AddRangeAsync(IEnumerable<TEntity> entities);
     Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate);
}
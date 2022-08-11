using System.Linq.Expressions;
using GNB.Domain.Entities;

namespace GNB.Domain.InfrastructureContracts;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
     Task<IEnumerable<TEntity>> ListAllAsync();
     Task AddRangeAsync(IEnumerable<TEntity> entities);
     Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate);
}
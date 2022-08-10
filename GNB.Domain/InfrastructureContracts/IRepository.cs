using System.Linq.Expressions;
using GNB.Domain.Entities;

namespace GNB.Domain.InfrastructureContracts;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
     Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken);
     Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
     Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate);
}
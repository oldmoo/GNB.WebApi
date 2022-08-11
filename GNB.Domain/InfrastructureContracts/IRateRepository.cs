using System.Linq.Expressions;
using GNB.Domain.Entities;

namespace GNB.Domain.InfrastructureContracts;

public interface IRateRepository : IRepository<Rate>
{
     Task<Rate?> GetExitingRate(Expression<Func<Rate?, bool>> predicate);
}
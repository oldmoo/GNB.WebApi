using GNB.Domain.Entities;

namespace GNB.Domain.DomainServicesContracts;

public interface IBaseService<T> where T : class
{
     Task<IEnumerable<T>> Get(CancellationToken token);
}
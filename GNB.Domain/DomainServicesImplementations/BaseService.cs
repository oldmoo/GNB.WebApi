using GNB.Domain.DomainServicesContracts;
using GNB.Domain.InfrastructureContracts;

namespace GNB.Domain.DomainServicesImplementations;

public class BaseService<T> : IBaseService<T> where T : class
{
     private readonly IService<T> _service;

     public BaseService(IService<T> service)
     {
          _service = service;
     }

     public async Task<IEnumerable<T>> Get()
     {
          return await _service.Get();
     }
     
}
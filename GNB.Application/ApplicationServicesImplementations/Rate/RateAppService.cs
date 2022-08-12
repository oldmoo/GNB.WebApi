using GNB.Application.ApplicationServicesContracts.Rate;
using GNB.Domain.DomainServicesContracts.Rate;

namespace GNB.Application.ApplicationServicesImplementations.Rate;

public class RateAppService : IRateAppService
{
     private readonly IRateDomainService _rateDomainService;

     public RateAppService(IRateDomainService rateDomainService)
     {
          _rateDomainService = rateDomainService;
     }

     public async Task<IEnumerable<Domain.Entities.Rate>> Get()
     {
          return await _rateDomainService.Get();
     }
}
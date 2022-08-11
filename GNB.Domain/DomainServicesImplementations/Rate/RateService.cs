using GNB.Domain.DomainServicesContracts;
using GNB.Domain.DomainServicesContracts.Rate;
using GNB.Domain.InfrastructureContracts;
using Microsoft.Extensions.Configuration;

namespace GNB.Domain.DomainServicesImplementations.Rate;

public class RateService : IRateService
{
     private readonly IService<Entities.Rate> _rateServiceExternal;
     private readonly IUnitOfWork _unitOfWork;

     public RateService(IService<Entities.Rate> rateServiceExternal, IUnitOfWork unitOfWork, IConfiguration configuration)
     {
          _rateServiceExternal = rateServiceExternal;
          _unitOfWork = unitOfWork;
          _rateServiceExternal.ClientName = configuration["RateClientName"];
     }
    

     public async Task<IEnumerable<Entities.Rate>> Get(CancellationToken token)
     {
         var rates = await _rateServiceExternal.Get(token);
         await AddRangeAsync(rates, token);
       
         return rates;
     }

     public async Task AddRangeAsync(IEnumerable<Entities.Rate> rates, CancellationToken cancellationToken)
     {
          await _unitOfWork.RateRepository.AddRangeAsync(rates, cancellationToken);
          await _unitOfWork.SaveChangesAsync();
     }
   
}
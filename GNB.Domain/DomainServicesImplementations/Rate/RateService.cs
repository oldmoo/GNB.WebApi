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
    

     public async Task<IEnumerable<Entities.Rate>> Get()
     {
         var rates = await _rateServiceExternal.Get();
         await AddRangeAsync(rates);
       
         return rates;
     }

     public async Task AddRangeAsync(IEnumerable<Entities.Rate> rates)
     {
          await _unitOfWork.RateRepository.AddRangeAsync(rates);
          await _unitOfWork.SaveChangesAsync();
     }
   
}
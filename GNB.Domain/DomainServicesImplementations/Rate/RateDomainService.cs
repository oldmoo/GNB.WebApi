using GNB.Domain.DomainServicesContracts.Rate;
using GNB.Domain.Enums;
using GNB.Domain.Helper;
using GNB.Domain.InfrastructureContracts;
using Microsoft.Extensions.Configuration;

namespace GNB.Domain.DomainServicesImplementations.Rate;

public class RateDomainService : IRateDomainService
{
     private readonly IService<Entities.Rate> _rateServiceExternal;
     private readonly IUnitOfWork _unitOfWork;

     public RateDomainService(IService<Entities.Rate> rateServiceExternal, IUnitOfWork unitOfWork, IConfiguration configuration)
     {
          _rateServiceExternal = rateServiceExternal;
          _unitOfWork = unitOfWork;
          _rateServiceExternal.ClientName = configuration["HttpClient:Rate"];
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
     
     public async Task<decimal> GetExistingRate(Currency from, Currency to)
     {
          var rate = await _unitOfWork.RateRepository.GetExitingRate(r => r != null && r.From == from && r.To == to);
          var rateInverse = await _unitOfWork.RateRepository.GetExitingRate(r => r != null && r.From == to && r.To == from);
         
          var rateValue = rate?.Value ?? 1 / rateInverse!.Value;
          return rateValue;
     }

     public async Task<decimal> GetRateByCurrency(Currency from, Currency to)
     {
          var currDictionary =
               HelperCurrency.GetCurrencyDictionary(await _unitOfWork.RateRepository.ListAllAsync());
          
          if (currDictionary is not null && currDictionary[from].Contains(to))
          {
               return await GetExistingRate(from, to);
          }

          foreach (var code in currDictionary?[from]!)
          {
               var rate = await GetRateByCurrency(code, to);
               if (rate != 0)
               {
                    return rate * await GetExistingRate(from, code);
               }
          }
          return 0;
     }
}
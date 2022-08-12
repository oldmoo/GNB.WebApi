using GNB.Application.Dtos;
using GNB.Domain.DomainServicesContracts;
using GNB.Domain.DomainServicesContracts.Rate;

namespace GNB.Application.ApplicationServicesContracts.Rate;

public interface IRateAppService 
{
     Task<IEnumerable<RateDto>> Get();
}
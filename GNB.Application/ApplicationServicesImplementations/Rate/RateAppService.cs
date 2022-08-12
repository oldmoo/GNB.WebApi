using AutoMapper;
using GNB.Application.ApplicationServicesContracts.Rate;
using GNB.Application.Dtos;
using GNB.Domain.DomainServicesContracts.Rate;

namespace GNB.Application.ApplicationServicesImplementations.Rate;

public class RateAppService : IRateAppService
{
     private readonly IRateDomainService _rateDomainService;
     private readonly IMapper _mapper;

     public RateAppService(IRateDomainService rateDomainService, IMapper mapper)
     {
          _rateDomainService = rateDomainService;
          _mapper = mapper;
     }

     public async Task<IEnumerable<RateDto>> Get()
     {
         var rates =  await _rateDomainService.Get();
         var ratesDto = _mapper.Map<IEnumerable<RateDto>>(rates);
         return ratesDto;
     }
}
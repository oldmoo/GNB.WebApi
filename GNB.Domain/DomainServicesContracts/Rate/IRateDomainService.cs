using GNB.Domain.Enums;

namespace GNB.Domain.DomainServicesContracts.Rate;

public interface IRateDomainService : IBaseService<Entities.Rate>
{
    Task AddRangeAsync(IEnumerable<Entities.Rate> rates);
    Task<decimal> GetExistingRate(Currency from, Currency to);
    Task<decimal> GetRateByCurrency(Currency from, Currency to);
}
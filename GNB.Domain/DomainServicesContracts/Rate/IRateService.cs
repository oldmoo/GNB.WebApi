namespace GNB.Domain.DomainServicesContracts.Rate;

public interface IRateService : IBaseService<Entities.Rate>
{
    Task AddRangeAsync(IEnumerable<Entities.Rate> rates, CancellationToken cancellationToken);
}
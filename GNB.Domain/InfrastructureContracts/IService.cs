namespace GNB.Domain.InfrastructureContracts;

public interface IService<T> where T : class
{
     string? ClientName { get; set; }
     Task<IEnumerable<T>> Get();
}
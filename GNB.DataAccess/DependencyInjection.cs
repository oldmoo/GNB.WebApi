using System.Diagnostics.CodeAnalysis;
using GNB.Domain.DomainServicesContracts;
using GNB.Domain.DomainServicesContracts.Rate;
using GNB.Domain.DomainServicesContracts.Transaction;
using GNB.Domain.DomainServicesImplementations;
using GNB.Domain.DomainServicesImplementations.Rate;
using GNB.Domain.DomainServicesImplementations.Transaction;
using GNB.Domain.InfrastructureContracts;
using GNB.Infrastructure.Contexts;
using GNB.Infrastructure.Repositories;
using GNB.Infrastructure.Services;
using GNB.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GNB.Infrastructure;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
     public static IServiceCollection AddInfrastructure(this IServiceCollection services)
     {
          var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
          
          _ = services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
          _ = services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
          _ = services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
          _ = services.AddScoped(typeof(IService<>), typeof(Service<>));
          _ = services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
          _ = services.AddScoped(typeof(IRateRepository), typeof(RateRepository));
          _ = services.AddScoped(typeof(ITransactionRepository), typeof(TransactionRepository));
          _ = services.AddScoped(typeof(IRateService), typeof(RateService));
          _ = services.AddScoped(typeof(ITransactionService), typeof(TransactionService));
          
          return services;
     }
     
}
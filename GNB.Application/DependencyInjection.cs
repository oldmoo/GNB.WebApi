using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GNB.Application.ApplicationServicesContracts.Rate;
using GNB.Application.ApplicationServicesContracts.Transaction;
using GNB.Application.ApplicationServicesImplementations;
using GNB.Application.ApplicationServicesImplementations.Rate;
using Microsoft.Extensions.DependencyInjection;

namespace GNB.Application;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
     public static IServiceCollection AddServicesApplication(this IServiceCollection services)
     {
          _ = services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
          _ = services.AddScoped(typeof(IRateAppService), typeof(RateAppService));
          _ = services.AddScoped(typeof(ITransactionAppService), typeof(TransactionAppService));
          return services;
     }
}
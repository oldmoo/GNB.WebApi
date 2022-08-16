using System.Linq.Expressions;
using GNB.Domain.Entities;
using GNB.Domain.InfrastructureContracts;
using GNB.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GNB.Infrastructure.Repositories;

public class RateRepository :  Repository<Rate>, IRateRepository
{
     public RateRepository(AppDbContext context) : base(context)
     {
     }

     public async Task<Rate?> GetExitingRate(Expression<Func<Rate?, bool>> predicate)
     {
          return await Context.Rates.SingleOrDefaultAsync(predicate);
     }
}
using System.Linq.Expressions;

namespace GNB.Infrastructure.Helpers;

public static class RepositoryHelper
{
     public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> source, IEnumerable<Expression<Func<TEntity, bool>>>? predicates) where TEntity : class
     {
          if (predicates is not null && predicates.Any())
               foreach (var predicate in predicates)
               {
                    source = source.Where(predicate);
               }
          return source;
     }
}
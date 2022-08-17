using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Client.Area
{
    public interface ISource<TId, TGetEntity, TPutEntity, TContext>
       where TGetEntity : class
       where TPutEntity : class
    {
        Task<TGetEntity[]> All(
                Expression<Func<TGetEntity, bool>> filter,
                TContext context,
                SourceOptions options,
                CancellationToken token
            );

        Task<SourceResult<TId, TGetEntity>> Single(TId id, TContext context, SourceOptions options, CancellationToken token);

        Task<TGetEntity[]> All(TContext context, SourceOptions options, CancellationToken token);

        Task<SourceResult<TId, TGetEntity>> Remove(TId id, TContext context, SourceOptions options, CancellationToken token);

        Task<SourceResult<TId, TPutEntity>> Insert(
                TPutEntity entity,
                TContext context,
                SourceOptions options,
                CancellationToken token
            );

        Task<SourceResult<TId, TPutEntity>> Update(
                TId id,
                TPutEntity entity,
                TContext context,
                SourceOptions options,
                CancellationToken token
            );
    }

    public interface ISource<TId, TEntity, TContext> : ISource<TId, TEntity, TEntity, TContext> where TEntity : class
    {

    }
}

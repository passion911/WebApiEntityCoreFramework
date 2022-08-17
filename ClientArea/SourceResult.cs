using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.Area
{
    public class SourceResult<TId, TEntity> where TEntity : class
    {
        internal SourceResult(
            SourceResultCode code,
            TId id,
            TEntity value = null,
            IReadOnlyDictionary<string, string[]> errors = null
        )
        {
            this.Errors = new Dictionary<string, string[]>();
            this.Code = code;
            this.Value = value;
            this.Id = id;
            if (errors != null)
            {
                this.Errors = errors.ToDictionary(a => a.Key, a => a.Value?.ToArray() ?? new string[0]);
            }
        }

        public SourceResultCode Code { get; }

        public bool IsSuccess => (int)this.Code < 100;

        public TEntity Value { get; }

        public TId Id { get; }

        public IReadOnlyDictionary<string, string[]> Errors { get; }

        public void ThrowIfNotSuccess()
        {
            if (!this.IsSuccess)
            {
                var message = JsonSerializer.Serialize(Errors);
                throw new SourceException(this.Code, message);
            }
        }

        public static SourceResult<TId, TEntity> NotFound(TId id, IReadOnlyDictionary<string, string[]> errors = null)
        {
            return new SourceResult<TId, TEntity>(SourceResultCode.NotFound, id, errors: errors);
        }

        public static SourceResult<TId, TEntity> Forbidden(TId id)
        {
            return new SourceResult<TId, TEntity>(SourceResultCode.Forbidden, id);
        }

        public static SourceResult<TId, TEntity> Ok(TId id, TEntity value)
        {
            return new SourceResult<TId, TEntity>(SourceResultCode.Success, id, value);
        }

        public static SourceResult<TId, TEntity> Inserted(TId id, TEntity value)
        {
            return new SourceResult<TId, TEntity>(SourceResultCode.Inserted, id, value);
        }

        public static SourceResult<TId, TEntity> Updated(TId id, TEntity value)
        {
            return new SourceResult<TId, TEntity>(SourceResultCode.Updated, id, value);
        }

        public static SourceResult<TId, TEntity> Removed(TId id, TEntity value)
        {
            return new SourceResult<TId, TEntity>(SourceResultCode.Removed, id, value);
        }

        public static SourceResult<TId, TEntity> Conflict(TId id, TEntity value, IReadOnlyDictionary<string, string[]> errors = null)
        {
            return new SourceResult<TId, TEntity>(SourceResultCode.Conflict, id, value, errors);
        }

        public static SourceResult<TId, TEntity> NotModified(TId id, TEntity value)
        {
            return new SourceResult<TId, TEntity>(SourceResultCode.NotModified, id, value);
        }

        public static SourceResult<TId, TEntity> InvalidVersion(TId id, TEntity value, IReadOnlyDictionary<string, string[]> errors = null)
        {
            return new SourceResult<TId, TEntity>(SourceResultCode.InvalidVersion, id, value, errors);
        }

        public static SourceResult<TId, TEntity> NotValid(
                TId id,
                TEntity value,
                IReadOnlyDictionary<string, string[]> errors
            )
        {
            return new SourceResult<TId, TEntity>(SourceResultCode.NotValid, id, value, errors);
        }

        public static SourceResult<TId, TModel> ConvertTo<TModel, TContext>(
                SourceResult<TId, TEntity> sourceResult,
                Func<TEntity, TContext, TModel> conversion,
                TContext context
            )
            where TModel : class
        {
            if (sourceResult == null)
            {
                throw new ArgumentNullException(nameof(sourceResult));
            }

            if (conversion == null)
            {
                throw new ArgumentNullException(nameof(conversion));
            }

            return new SourceResult<TId, TModel>(sourceResult.Code,
                sourceResult.Id,
                sourceResult.Value == null ? null : conversion(sourceResult.Value, context),
                    sourceResult.Errors
                    );
        }

        public static SourceResult<TModelId, TModel> ConvertTo<TModelId, TModel, TContext>(
                SourceResult<TId, TEntity> sourceResult,
                Func<TId, TContext, TModelId> idConversion,
                Func<TEntity, TContext, TModel> conversion,
                TContext context
            )
            where TModel : class
        {
            if (sourceResult == null)
            {
                throw new ArgumentNullException(nameof(sourceResult));
            }

            if (conversion == null)
            {
                throw new ArgumentNullException(nameof(conversion));
            }

            if (idConversion == null)
            {
                throw new ArgumentNullException(nameof(idConversion));
            }

            return new SourceResult<TModelId, TModel>(sourceResult.Code,
                idConversion(sourceResult.Id, context),
                sourceResult.Value == null ? null : conversion(sourceResult.Value, context),
                    sourceResult.Errors);
        }

        public SourceResult<TId, TModel> ConvertTo<TModel, TContext>(
                Func<TEntity, TContext, TModel> conversion,
                TContext context
            )
            where TModel : class
        {
            return ConvertTo(this, conversion, context);
        }

        public SourceResult<TModelId, TModel> ConvertTo<TModelId, TModel, TContext>(
                Func<TId, TContext, TModelId> idConversion,
                Func<TEntity, TContext, TModel> conversion,
                TContext context
            )
            where TModel : class
        {
            return ConvertTo(this, idConversion, conversion, context);
        }
    }
}

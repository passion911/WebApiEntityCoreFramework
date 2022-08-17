using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Storage
{
    public class CacheOptions
    {
        public TimeSpan? SlidingExpiration { get; set; }

        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; } = TimeSpan.FromMinutes(10);

        public MemoryCacheEntryOptions ToMemoryCacheEntryOptions()
        {
            return new MemoryCacheEntryOptions
            {
                SlidingExpiration = this.SlidingExpiration,
                AbsoluteExpirationRelativeToNow = this.AbsoluteExpirationRelativeToNow
            };
        }
    }
}

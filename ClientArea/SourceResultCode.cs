using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Area
{
    public enum SourceResultCode
    {
        Success = 1,
        Updated = 2,
        Inserted = 3,
        Removed = 4,
        NotModified = 5,

        NotFound = 100,
        Conflict = 101,
        NotValid = 102,
        InvalidVersion = 103,
        Forbidden = 104
    }
}

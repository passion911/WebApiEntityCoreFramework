using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Area
{
    public class UserOwnerFilterOptions
    {
        private UserOwnerFilterOptions(bool ignore, Type entityType)
        {
            Ingore = ignore;
            EntityType = entityType;
        }

        public bool Ingore { get; }

        public Type EntityType { get; }

        public static UserOwnerFilterOptions Setup<T>(bool ignoreFilter)
        {
            return new UserOwnerFilterOptions(ignoreFilter, typeof(T));
        }
    }
}

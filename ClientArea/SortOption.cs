using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Area
{
    public class SortOption
    {
        public SortOption(string field, bool isAscending)
        {
            if (string.IsNullOrEmpty(field))
            {
                throw new ArgumentException("Cannot be null or empty.", nameof(field));
            }

            this.Field = field;
            this.IsAscending = isAscending;
        }

        public string Field { get; }

        public bool IsAscending { get; }
    }
}

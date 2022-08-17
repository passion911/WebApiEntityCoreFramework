using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Area
{
    public class SourceOptions
    {
        internal SourceOptions(
           string[] selectFields,
           int? pageSize,
           int? pageIndex,
           SortOption[] sortOptions,
           UserOwnerFilterOptions userOwnerFilterOptions,
           bool? obsoleteMaterialIncluded = false,
           bool? isDeletedEntityInclude = false
           )
        {
            this.SelectFields = selectFields?.ToArray() ?? new string[0];
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.Sort = sortOptions?.ToArray() ?? new SortOption[0];
            this.UserOwnerFilterOptions = userOwnerFilterOptions;
            this.ObsoleteMaterialIncluded = obsoleteMaterialIncluded ?? false;
            this.DeletedEntitiesIncluded = isDeletedEntityInclude ?? false;
        }

        public string[] SelectFields { get; }

        public bool HasSelect => this.SelectFields?.Any() == true;

        public int? PageSize { get; }

        public int? PageIndex { get; }

        public bool HasPaging => this.PageIndex.HasValue && this.PageSize.HasValue;

        public SortOption[] Sort { get; }

        public bool HasSort => this.Sort?.Any() == true;

        public UserOwnerFilterOptions UserOwnerFilterOptions { get; }

        public bool DeletedEntitiesIncluded { get; set; }

        public bool ObsoleteMaterialIncluded { get; set; }

        public static SourceOptions Empty => new SourceOptions(new string[0], null, null, new SortOption[0], null, null);
    }
}

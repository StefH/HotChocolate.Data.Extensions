using HotChocolate.Data.Filters.Enums;

namespace HotChocolate.Data.Filters.Models
{
    internal struct ExtendedOperationDetail
    {
        public ExtendedFilterOperations Key { get; set; }

        public int Id => (int)Key;

        public bool Overwrite { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
using System.Collections.Generic;
using HotChocolate.Data.Filters.Models;
using OD = HotChocolate.Data.Filters.Models.ExtendedOperationDetail;
using FO = HotChocolate.Data.Filters.Enums.ExtendedFilterOperation;

namespace HotChocolate.Data.Filters
{
    internal class ExtendedFilterOperationHelper
    {
        public IDictionary<FO, OD> Operations = new Dictionary<FO, OD>();

        public ExtendedFilterOperationHelper(ExtendedFilterConfiguration cfg)
        {
            Operations.Add(FO.StringContainsIgnoreCase, new OD { Key = FO.StringContainsIgnoreCase, Name = "containsIgnoreCase", Overwrite = cfg.OverwriteStringContains });
            Operations.Add(FO.StringNotContainsIgnoreCase, new OD { Key = FO.StringNotContainsIgnoreCase, Name = "ncontainsIgnoreCase", Overwrite = cfg.OverwriteStringNotContains });

            Operations.Add(FO.StringEqualsIgnoreCase, new OD { Key = FO.StringEqualsIgnoreCase, Name = "eqIgnoreCase", Overwrite = cfg.OverwriteStringEquals });
            Operations.Add(FO.StringNotEqualsIgnoreCase, new OD { Key = FO.StringNotEqualsIgnoreCase, Name = "neqIgnoreCase", Overwrite = cfg.OverwriteStringNotEquals });

            Operations.Add(FO.StringEndsWithIgnoreCase, new OD { Key = FO.StringEndsWithIgnoreCase, Name = "endsWithIgnoreCase", Overwrite = cfg.OverwriteStringEndsWith });
            Operations.Add(FO.StringNotEndsWithIgnoreCase, new OD { Key = FO.StringNotEndsWithIgnoreCase, Name = "nendsWithIgnoreCase", Overwrite = cfg.OverwriteStringNotEndsWith });

            Operations.Add(FO.StringStartsWithIgnoreCase, new OD { Key = FO.StringStartsWithIgnoreCase, Name = "startsWithIgnoreCase", Overwrite = cfg.OverwriteStringStartsWith });
            Operations.Add(FO.StringNotStartsWithIgnoreCase, new OD { Key = FO.StringNotStartsWithIgnoreCase, Name = "nstartsWithIgnoreCase", Overwrite = cfg.OverwriteStringNotStartsWith });
        }
    }
}
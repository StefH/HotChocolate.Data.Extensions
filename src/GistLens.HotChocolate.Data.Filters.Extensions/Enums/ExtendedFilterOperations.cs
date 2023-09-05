namespace HotChocolate.Data.Filters.Enums
{
    internal enum ExtendedFilterOperation

    {
        StringEqualsIgnoreCase = 2000000,
        StringNotEqualsIgnoreCase = 2000001,

        StringContainsIgnoreCase = 2000010,
        StringNotContainsIgnoreCase = 2000011,

        StringEndsWithIgnoreCase = 2000020,
        StringNotEndsWithIgnoreCase = 2000021,

        StringStartsWithIgnoreCase = 2000030,
        StringNotStartsWithIgnoreCase = 2000031,
    }
}
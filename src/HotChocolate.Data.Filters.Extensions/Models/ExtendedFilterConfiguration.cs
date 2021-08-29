namespace HotChocolate.Data.Filters.Models
{
    public class ExtendedFilterConfiguration
    {
        public bool OverwriteStringContains { get; set; } = false;
        public bool OverwriteStringNotContains { get; set; } = false;

        public bool OverwriteStringEquals { get; set; } = false;
        public bool OverwriteStringNotEquals { get; set; } = false;

        public bool OverwriteStringEndsWith { get; set; } = false;
        public bool OverwriteStringNotEndsWith { get; set; } = false;

        public bool OverwriteStringStartsWith { get; set; } = false;
        public bool OverwriteStringNotStartsWith { get; set; } = false;
    }
}
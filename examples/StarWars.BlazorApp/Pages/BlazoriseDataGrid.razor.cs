using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using StarWarsGeneratedClient;
using Character = StarWarsGeneratedClient.IGetCharactersWithPaging_CharactersWithPagingFilteringAndSorting_Items;

namespace StarWars.BlazorApp.Pages
{
    public partial class BlazoriseDataGrid
    {
        private IReadOnlyList<Character?> characters = new List<Character?>();
        private int total = 0;

        async Task OnReadData(DataGridReadDataEventArgs<Character> e)
        {
            var sortColumn = e.Columns.FirstOrDefault(c => c.SortDirection != SortDirection.None);
            var sorts = new List<ICharacterSortInput>();
            if (TryParseSortColumn<ICharacterSortInput>(sortColumn, out var sort))
            {
                sorts.Add(sort);
            }

            var filterColumns = e.Columns.Where(c => c.SearchValue is not null).ToList();
            var filter = ParseFilterInput<ICharacterFilterInput>(filterColumns);

            var operationResult = await Client.GetCharactersWithPaging.ExecuteAsync(e.PageSize, (e.Page - 1) * e.PageSize, filter, sorts);
            if (operationResult?.Data?.CharactersWithPagingFilteringAndSorting is not null)
            {
                var result = operationResult.Data.CharactersWithPagingFilteringAndSorting;
                characters = result.Items is not null ? result.Items : new List<Character?>();
                total = result?.TotalCount ?? 0;

                // always call StateHasChanged!
                StateHasChanged();
            }
        }

        private static T? ParseFilterInput<T>(IEnumerable<DataGridColumnInfo> columns) where T : new()
        {
            var filters = new List<T>();
            foreach (var column in columns)
            {
                if (TryParseFilterColumn<T>(column, out var subFilter))
                {
                    filters.Add(subFilter);
                }
            }

            if (filters.Any())
            {
                if (filters.Count == 1)
                {
                    return filters.Single();
                }

                var property = typeof(T).GetProperty("And")!;
                var filter = new T();
                property.SetValue(filter, filters);

                return filter;
            }

            return default;
        }

        private static bool TryParseFilterColumn<T>(DataGridColumnInfo column, [NotNullWhen(true)] out T? filter) where T : new()
        {
            filter = default;

            var stringvalue = column.SearchValue as string;
            if (string.IsNullOrEmpty(stringvalue))
            {
                return false;
            }

            var property = typeof(T).GetProperty(column.Field);
            if (property is null)
            {
                return false;
            }

            //AnyOf<ComparableInt32OperationFilterInput, ComparableDoubleOperationFilterInput, StringOperationFilterInput>? filterInput;
            object? filterInput;
            switch (column.ColumnType)
            {
                case DataGridColumnType.Numeric:
                    if (property.PropertyType == typeof(ComparableInt32OperationFilterInput))
                    {
                        filterInput = new ComparableInt32OperationFilterInput
                        {
                            Gte = int.Parse(stringvalue)
                        };
                    }
                    else
                    {
                        filterInput = new ComparableDoubleOperationFilterInput
                        {
                            Gte = double.Parse(stringvalue)
                        };
                    }
                    break;

                default:
                    filterInput = new StringOperationFilterInput
                    {
                        //Contains = stringvalue
                        ContainsIgnoreCase = stringvalue
                    };
                    break;
            }

            if (filterInput is not null)
            {
                filter = new T();
                property.SetValue(filter, filterInput);

                return true;
            }

            return false;
        }

        private static bool TryParseSortColumn<T>(DataGridColumnInfo? column, [NotNullWhen(true)] out T? sort) where T : new()
        {
            sort = default;

            if (column is null)
            {
                return false;
            }

            var property = typeof(T).GetProperty(column.Field);
            if (property is null)
            {
                return false;
            }

            sort = new T();
            property.SetValue(sort, Parse(column.SortDirection));
            return true;
        }

        private static SortEnumType Parse(SortDirection direction)
        {
            return direction == SortDirection.Ascending ? SortEnumType.Asc : SortEnumType.Desc;
        }
    }
}

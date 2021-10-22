# HotChocolate.Data.Extensions

Contains some extra String filters like:
- eqIgnoreCase / neqIgnoreCase
- containsIgnoreCase / ncontainsIgnoreCase
- endsWithIgnoreCase / nendsWithIgnoreCase
- startsWithIgnoreCase / nstartsWithIgnoreCase

## NuGet packages

| Name | NuGet | Info |
|:- |:- |:- |
| `HotChocolate.Data.Extensions` | [![NuGet Badge](https://buildstats.info/nuget/HotChocolate.Data.Extensions)](https://www.nuget.org/packages/HotChocolate.Data.Extensions) | Combined NuGet
| `HotChocolate.Data.Filters.Extensions` | [![NuGet Badge](https://buildstats.info/nuget/HotChocolate.Data.Filters.Extensions)](https://www.nuget.org/packages/HotChocolate.Data.Filters.Extensions) | Contains only extensions for Filters

## Usage "HotChocolate.Data.Filters.Extensions"

### Register in your Startup.cs

``` c#
    .AddGraphQLServer()

    // ...
           
        // Add filtering and sorting capabilities.
        .AddExtendedFiltering() // 👈 Instead of .AddFiltering()

    // ...
```

### Use in GraphQL

Now you can write GraphQL like this:

``` gql
query GetCharactersWithPaging1(
  $take: Int
  $skip: Int
  $order: [ICharacterSortInput!]
) {
  charactersWithPagingFilteringAndSorting(
    take: $take
    skip: $skip
    where: { name: { containsIgnoreCase: "c" } } # 👈 instead of contains
    order: $order
  ) {
    items {
      ...c
    }
    totalCount
    pageInfo {
      hasNextPage
      hasPreviousPage
    }
  }
}

query GetCharactersWithPaging2(
  $take: Int
  $skip: Int
  $order: [ICharacterSortInput!]
) {
  charactersWithPagingFilteringAndSorting(
    take: $take
    skip: $skip
    where: { name: { eqIgnoreCase: "c-3PO" } } # 👈 instead of eq
    order: $order
  ) {
    items {
      ...c
    }
    totalCount
    pageInfo {
      hasNextPage
      hasPreviousPage
    }
  }
}

fragment c on Character {
  id
  name
  height
  appearsIn
}
```

# ![icon](./resources/icon_32x32.png) HotChocolate.Data.Extensions

Contains some extra String filters like:
- `eqIgnoreCase` / `neqIgnoreCase`
- `containsIgnoreCase` / `ncontainsIgnoreCase`
- `endsWithIgnoreCase` / `nendsWithIgnoreCase`
- `startsWithIgnoreCase` / `nstartsWithIgnoreCase`

## NuGet packages

| Name | NuGet | Info |
|:- |:- |:- |
| `HotChocolate.Data.Extensions` | [![NuGet Badge](https://img.shields.io/nuget/v/HotChocolate.Data.Extensions)](https://www.nuget.org/packages/HotChocolate.Data.Extensions) | Combined NuGet
| `HotChocolate.Data.Filters.Extensions` | [![NuGet Badge](https://img.shields.io/nuget/v/HotChocolate.Data.Filters.Extensions)](https://www.nuget.org/packages/HotChocolate.Data.Filters.Extensions) | Contains only extensions for Filters

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


## Sponsors

[Entity Framework Extensions](https://entityframework-extensions.net/?utm_source=StefH) and [Dapper Plus](https://dapper-plus.net/?utm_source=StefH) are major sponsors and proud to contribute to the development of **HotChocolate.Data.Extensions** and **HotChocolate.Data.Filters.Extensions**.

[![Entity Framework Extensions](https://raw.githubusercontent.com/StefH/resources/main/sponsor/entity-framework-extensions-sponsor.png)](https://entityframework-extensions.net/bulk-insert?utm_source=StefH)

[![Dapper Plus](https://raw.githubusercontent.com/StefH/resources/main/sponsor/dapper-plus-sponsor.png)](https://dapper-plus.net/bulk-insert?utm_source=StefH)
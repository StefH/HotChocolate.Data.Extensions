## Usage

### Register in your Startup.cs

``` c#
    .AddGraphQLServer()

    // ...
           
        .AddExtendedFiltering() // 👈 Instead of .AddFiltering()

    // ...
```

### GraphQL

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
      id
      name
      height
      appearsIn
    }
    totalCount
    pageInfo {
      hasNextPage
      hasPreviousPage
    }
  }
}
```


### Sponsors

[Entity Framework Extensions](https://entityframework-extensions.net/?utm_source=StefH) and [Dapper Plus](https://dapper-plus.net/?utm_source=StefH) are major sponsors and proud to contribute to the development of **HotChocolate.Data.Filters.Extensions**.

[![Entity Framework Extensions](https://raw.githubusercontent.com/StefH/resources/main/sponsor/entity-framework-extensions-sponsor.png)](https://entityframework-extensions.net/bulk-insert?utm_source=StefH)

[![Dapper Plus](https://raw.githubusercontent.com/StefH/resources/main/sponsor/dapper-plus-sponsor.png)](https://dapper-plus.net/bulk-insert?utm_source=StefH)
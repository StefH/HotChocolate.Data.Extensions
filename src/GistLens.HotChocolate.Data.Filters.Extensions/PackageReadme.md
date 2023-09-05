# Usage

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
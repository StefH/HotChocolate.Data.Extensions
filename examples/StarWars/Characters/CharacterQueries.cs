using System;
using System.Collections.Generic;
using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Data.Sorting.Expressions;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using StarWars.Repositories;

namespace StarWars.Characters
{
    /// <summary>
    /// The queries related to characters.
    /// </summary>
    [ExtendObjectType(OperationTypeNames.Query)]
    public class CharacterQueries
    {
        /// <summary>
        /// Retrieve a hero by a particular Star Wars episode.
        /// </summary>
        /// <param name="episode">The episode to retrieve the hero.</param>
        /// <param name="repository">The character repository.</param>
        /// <returns>The hero character.</returns>
        public ICharacter GetHero(
            Episode episode,
            [Service] ICharacterRepository repository) =>
            repository.GetHero(episode);

        /// <summary>
        /// Retrieve all heros by a particular Star Wars episode.
        /// </summary>
        /// <param name="episode">The episode to retrieve the hero.</param>
        /// <param name="repository">The character repository.</param>
        /// <returns>The hero characters.</returns>
        public IEnumerable<ICharacter> GetHeros(
            Episode episode,
            [Service] ICharacterRepository repository) =>
            repository.GetHeros(episode);

        /// <summary>
        /// Gets all character.
        /// </summary>
        /// <param name="repository">The character repository.</param>
        /// <returns>The character.</returns>
        public IEnumerable<ICharacter> GetCharacters(
            [Service] ICharacterRepository repository) =>
            repository.GetCharacters();

        /// <summary>
        /// Gets all character.
        /// </summary>
        /// <param name="repository">The character repository.</param>
        /// <returns>The character.</returns>
        [UsePaging(typeof(InterfaceType<ICharacter>), IncludeTotalCount = true, AllowBackwardPagination = true)]
        [UseFiltering]
        [UseSorting]
        public IEnumerable<ICharacter> GetCharactersWithCursorPagingFilteringAndSorting(
            [Service] ICharacterRepository repository) =>
            repository.GetCharacters();

        /// <summary>
        /// Gets all characters.
        /// </summary>
        /// <param name="repository">The character repository.</param>
        /// <returns>The character.</returns>
        [UseOffsetPaging(typeof(InterfaceType<ICharacter>), IncludeTotalCount = true)] // , AllowBackwardPagination = true
        [UseFiltering]
        [UseSorting]
        public IEnumerable<ICharacter> GetCharactersWithPagingFilteringAndSorting(
            [Service] ICharacterRepository repository) =>
            repository.GetCharacters();

        /// <summary>
        /// Gets all characters.
        /// https://stackoverflow.com/questions/68459215/hot-chocolate-graphql-interceptor-middleware-to-get-iqueryable-before-data-fet
        /// </summary>
        /// <param name="repository">The character repository.</param>
        /// <param name="context">The ResolverContext</param>
        /// <returns>The character.</returns>
        // [UseOffsetPaging(typeof(InterfaceType<ICharacter>), IncludeTotalCount = true)] // , AllowBackwardPagination = true
        [UseFiltering]
        [UseSorting]
        public IEnumerable<ICharacter> GetCharactersWithPagingFilteringAndSortingCustom(
            IResolverContext context,
            [Service] ICharacterRepository repository)
        {
            var charactersAsQueryable = repository.GetCharacters();

            if (context.LocalContextData.TryGetValue(QueryableFilterProvider.ContextApplyFilteringKey, out var value))
            {
                if (value is ApplyFiltering applyFiltering)
                {
                    var obj = applyFiltering(context, charactersAsQueryable);
                    if (obj is IQueryable<ICharacter> queryable)
                    {
                        return queryable.Sort(context); // also sort
                    }

                    throw new Exception("ThrowHelper.Filtering_TypeMissmatch(context, expectedType, obj.GetType()");
                }
            }

            throw new Exception("ThrowHelper.Filtering_FilteringWasNotFound(context)");
        }

        /// <summary>
        /// Gets characters by it`s id.
        /// </summary>
        /// <param name="ids">The ids of the human to retrieve.</param>
        /// <param name="repository">The character repository.</param>
        /// <returns>The characters.</returns>
        public IEnumerable<ICharacter> GetCharactersByIds(
            int[] ids,
            [Service] ICharacterRepository repository) =>
            repository.GetCharacters(ids);

        /// <summary>
        /// Search the repository for objects that contain the text.
        /// </summary>
        /// <param name="text">
        /// The text we are searching for.
        /// </param>
        /// <param name="repository">The character repository.</param>
        /// <returns>Returns the union type <see cref="ISearchResult"/>.</returns>
        public IEnumerable<ISearchResult> Search(
            string text,
            [Service] ICharacterRepository repository) =>
            repository.Search(text);
    }
}

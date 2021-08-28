using System.Collections.Generic;
using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.Extensions.Logging;
using StarWars.Repositories;

namespace StarWars.Characters
{
    /// <summary>
    /// This resolver class extends all object types implementing <see cref="ICharacter"/>.
    /// </summary>
    [ExtendObjectType(typeof(ICharacter))]
    public class CharacterResolvers
    {
        private readonly ILogger<CharacterResolvers> _logger;

        public CharacterResolvers(ILogger<CharacterResolvers> logger)
        {
            _logger = logger;
        }

        [UsePaging(typeof(InterfaceType<ICharacter>))]
        [BindMember(nameof(ICharacter.Friends))]
        public IEnumerable<ICharacter> GetFriends(
            [Parent] ICharacter character,
            [Service] ICharacterRepository repository)
        {
            _logger.LogInformation("GetFriends for ids : {ids}", string.Join(",", character.Friends));

            return repository.GetCharacters(character.Friends.ToArray());
        }
    }
}
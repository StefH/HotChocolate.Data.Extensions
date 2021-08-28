using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StarWarsGeneratedClient;

namespace graphql_console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddHttpClient(
                "StarWarsGeneratedClient",
                c => c.BaseAddress = new Uri("https://localhost:44339/starwars"));
            serviceCollection.AddStarWarsGeneratedClient();

            IServiceProvider services = serviceCollection.BuildServiceProvider();
            var client = services.GetRequiredService<IStarWarsGeneratedClient>();
            
            var getHeroResult = await client.GetHero.ExecuteAsync(Episode.Empire);

            var heroAsJson = JsonSerializer.Serialize(getHeroResult.Data.Hero, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(heroAsJson);

            var charactersByIdsResult = await client.GetCharactersByIds.ExecuteAsync(new[] { 1000, 2000});

            var charactersByIdsJson = JsonSerializer.Serialize(charactersByIdsResult.Data.CharactersByIds, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(charactersByIdsJson);

            // Mutate
            var createReviewResult = await client.CreateReview.ExecuteAsync(new CreateReviewInput { Episode = Episode.Empire, Commentary = "test", Stars = 5 });
            Console.WriteLine(createReviewResult.Data.CreateReview.Review.Stars);
        }
    }
}
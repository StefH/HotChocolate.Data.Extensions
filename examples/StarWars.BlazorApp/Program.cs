using System;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
// using Character = StarWarsGeneratedClient.IGetCharactersWithPaging_CharactersWithPagingFilteringAndSorting_Items;

namespace StarWars.BlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services
              .AddBlazorise(options =>
              {
                  options.ChangeTextOnKeyPress = true;
              })
              .AddBootstrapProviders()
              .AddFontAwesomeIcons();

            // builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient("StarWarsGeneratedClient", c => c.BaseAddress = new Uri("https://localhost:44339/starwars"));
            builder.Services.AddStarWarsGeneratedClient();

            await builder.Build().RunAsync();
        }
    }
}

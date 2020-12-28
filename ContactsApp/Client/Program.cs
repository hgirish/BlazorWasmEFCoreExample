using ContactsApp.BaseRepository;
using ContactsApp.Client.Data;
using ContactsApp.Controls;
using ContactsApp.Controls.Grid;
using ContactsApp.Model;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContactsApp.Client
{
    public class Program
    {
        public const string BaseClient = "ContactsApp.ServerAPI";

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient(BaseClient,
                client =>
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            //    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            //builder.Services.AddScoped(sp => 
            //new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>()
            .CreateClient(BaseClient));


            builder.Services.AddApiAuthorization();

            // client implementation
            builder.Services.AddScoped<IBasicRepository<Contact>, WasmRepository>();
            builder.Services.AddScoped<IUnitOfWork<Contact>, WasmUnitOfWork>();

            // references to control filters and sorts
            builder.Services.AddScoped<IPageHelper, PageHelper>();
            builder.Services.AddScoped<IContactFilters, ContactFilters>();
            builder.Services.AddScoped<GridQueryAdapter>();
            builder.Services.AddScoped<EditService>();

            builder.Services.AddScoped(sp =>
            new ClaimsPrincipal(new ClaimsIdentity()));

            await builder.Build().RunAsync();
        }
    }
}

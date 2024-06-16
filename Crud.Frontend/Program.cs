using Crud.Frontend;
using Crud.Frontend.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IProductServices, ProductServices>();

builder.Services.AddHttpClient("webapi", cfg =>
{
    cfg.BaseAddress = new Uri("https://localhost:7130/");
});


await builder.Build().RunAsync();

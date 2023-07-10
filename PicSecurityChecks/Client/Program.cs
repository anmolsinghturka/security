using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PicSecurityChecks.Client;
using PicSecurityChecks.Client.Interfaces;
using PicSecurityChecks.Client.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
var uri = builder.Configuration.GetSection("LocalApiUrl:Uri").Value;
var nicheUri = builder.Configuration.GetSection("NDS:Connection").Value;
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient<ISecurityCheckNamesDataService, SecurityCheckDataService>(c => c.BaseAddress = new Uri(uri));
builder.Services.AddHttpClient<IFlaggedNamesDataService, FlaggedNamesDataService>(c => c.BaseAddress = new Uri(uri));
builder.Services.AddHttpClient<ISearchFlaggedNamesDataService, SearchFlaggedNamesDataService>(c => c.BaseAddress = new Uri(uri));
builder.Services.AddHttpClient<IInetDataService, InetDataService>(c => c.BaseAddress = new Uri(uri));
builder.Services.AddHttpClient<ICurrentUserDataService, CurrentUserDataService>(c => c.BaseAddress = new Uri(uri));
builder.Services.AddHttpClient<INicheCallsDataService, NicheCallDataService>(c => c.BaseAddress = new Uri(uri));

        

await builder.Build().RunAsync();

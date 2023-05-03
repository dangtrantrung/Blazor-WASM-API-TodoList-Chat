using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using blazingchat.Client;
using BlazingChat.Shared;
using Blazored.Toast;
using BlazingChat.Shared.Services;
using System.Security.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
//từ net6.0 phải cấu hình server Kestrel: httpsDefaultoption, ssl protocol để phù hợp win7 ssl protocol (tls 1.2, 1.3)

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// add Uri to connect to the Todolist.API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7283") });
builder.Services.AddTransient<ITaskAPIClient,TaskAPIClient>();


builder.Services.AddScoped<IBananaService,BananaService>();
builder.Services.AddScoped<IUnitService,UnitService>();
builder.Services.AddBlazoredToast();
await builder.Build().RunAsync();

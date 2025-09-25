using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using TicketsSystemBlazorApp.Providers;
using TicketsSystemBlazorApp.Service;
using TicketsSystemBlazorApp.Service.Authentication;
using TicketsSystemBlazorApp.Service.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();

// Register the API client (NSwag)
builder.Services.AddHttpClient<IClient, Client>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7171/"); // ? your Web API base URL
});

// Register services
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<ClientAccountService>();
builder.Services.AddScoped<TicketServiceWrapper>();



// Auth state provider
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();



// Named HttpClient (optional)
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7171/");
});

// Register the handler itself
//builder.Services.AddTransient<AuthHeaderHandler>();




var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();

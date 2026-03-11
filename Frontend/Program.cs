using Frontend.Components;
using Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var backendUrl = builder.Configuration["BackendUrl"] ?? "http://localhost:5133";
builder.Services.AddHttpClient<ArtistApiService>(client =>
{
    client.BaseAddress = new Uri(backendUrl);
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpClient("auth", client =>
{
    client.BaseAddress = new Uri(backendUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

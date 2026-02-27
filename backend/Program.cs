var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<ArtistService>(); // Singleton - vienas sąrašas visai app
builder.Services.AddControllers();

builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.WithOrigins("https://localhost:7001") // Frontend portas
     .AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.MapControllers();

app.UseHttpsRedirection();

app.Run();
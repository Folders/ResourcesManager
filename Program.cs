using ResourcesManager.Components;
using ResourcesManager.Datas;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Database
builder.Services.AddDatabaseConfiguration(builder.Configuration);


var app = builder.Build();


// Database init
await app.InitializeDatabaseAsync();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


/*
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); 
await using var conn = new NpgsqlConnection(connectionString);
try
{
    await conn.OpenAsync();
    Console.WriteLine("Connexion réussie !");
}
catch (Exception ex)
{
    Console.WriteLine($"Erreur : {ex.Message}");
}
*/

app.Run();

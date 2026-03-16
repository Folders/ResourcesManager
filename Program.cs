using ResourcesManager.Components;
using ResourcesManager.Datas;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

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

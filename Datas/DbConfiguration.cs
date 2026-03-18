using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ResourcesManager.Datas
{
    public static class DbConfiguration
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var dbType = GetDbType(configuration);
            var connectionString = GetConnectionString(configuration, dbType);

            services.AddDbContext<AppDbContext>(options =>
            {
                switch (dbType)
                {
                    case DatabaseType.Postgres:
                        options.UseNpgsql(connectionString);
                        break;

                    case DatabaseType.SqlServer:
                        options.UseSqlServer(connectionString);
                        break;

                    default:
                        throw new InvalidOperationException($"Unsupported database type: {dbType}");
                }
            });

            return services;
        }

        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var dbType = GetDbType(configuration);

            switch (dbType)
            {
                case DatabaseType.Postgres:
                    await dbContext.Database.MigrateAsync();
                    break;

                case DatabaseType.SqlServer:
                    await dbContext.Database.EnsureCreatedAsync();
                    break;

                default:
                    throw new InvalidOperationException($"Unsupported database type: {dbType}");
            }
        }

        private static DatabaseType GetDbType(IConfiguration configuration)
        {
            var rawValue =
                configuration["DB_TYPE"]
                ?? configuration["Database:Provider"]
                ?? throw new InvalidOperationException("Database type is not configured.");

            return rawValue.Trim().ToLowerInvariant() switch
            {
                "postgres" or "postgresql" or "npgsql" => DatabaseType.Postgres,
                "sqlserver" or "mssql" => DatabaseType.SqlServer,
                _ => throw new InvalidOperationException($"Unsupported DB_TYPE '{rawValue}'.")
            };
        }

        private static string GetConnectionString(IConfiguration configuration, DatabaseType dbType)
        {
            var directConnectionString = configuration.GetConnectionString("DefaultConnection");

            if (!string.IsNullOrWhiteSpace(directConnectionString))
                return directConnectionString;

            var host = configuration["DB_HOST"];
            var port = configuration["DB_PORT"];
            var database = configuration["DB_NAME"];
            var user = configuration["DB_USER"];
            var password = configuration["DB_PASS"];

            if (string.IsNullOrWhiteSpace(host) ||
                string.IsNullOrWhiteSpace(port) ||
                string.IsNullOrWhiteSpace(database) ||
                string.IsNullOrWhiteSpace(user) ||
                string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException(
                    "Database connection is not configured. " +
                    "Either set ConnectionStrings:DefaultConnection or DB_HOST/DB_PORT/DB_NAME/DB_USER/DB_PASS.");
            }

            return dbType switch
            {
                DatabaseType.Postgres =>
                    $"Host={host};Port={port};Database={database};Username={user};Password={password}",

                DatabaseType.SqlServer =>
                    $"Server={host},{port};Database={database};User Id={user};Password={password};TrustServerCertificate=True",

                _ => throw new InvalidOperationException($"Unsupported database type: {dbType}")
            };
        }

        private enum DatabaseType
        {
            Postgres,
            SqlServer
        }
    }
}

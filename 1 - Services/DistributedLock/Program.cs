namespace DistributedLock;

using DTO;
using ILockDataAccess;
using LockDataAccess;

/// <summary>
///     Represents the entry point of the program.
/// </summary>
public class Program
{
    /// <summary>
    ///     The entry point for the application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder);
        var app = builder.Build();
        ConfigurePipeline(app);
        ConfigureRoutes(app);
        app.Run();
    }

    /// <summary>
    ///     Configures the services required for the application.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder used to build the application.</param>
    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        builder.Services.AddSingleton(configuration.GetSection(nameof(DbConnectionSettings)).Get<DbConnectionSettings>());
        builder.Services.AddSingleton<IDataLockDbContext, DataLockDbContext>();
    }

    /// <summary>
    ///     Configures the HTTP request pipeline of the application.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication" /> object representing the current application.</param>
    private static void ConfigurePipeline(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
    }

    /// <summary>
    ///     Configures the routes for handling HTTP requests in the web application.
    /// </summary>
    /// <param name="app">The <c>WebApplication</c> instance.</param>
    private static void ConfigureRoutes(WebApplication app)
    {
        app.MapGet("/locks", async (IDataLockDbContext dataLockDbContext) => Results.Ok(await dataLockDbContext.GetLocks()));
        app.MapGet("/locks/{key}",
            async (IDataLockDbContext dataLockDbContext, string key) => Results.Ok(await dataLockDbContext.GetLock(key)));

        app.MapGet("/locks/{key}/{machineName}/{ownerId}",
            async (IDataLockDbContext dataLockDbContext, string key, string machineName, string ownerId) =>
                await dataLockDbContext.AcquireLock(key, machineName, ownerId));

        app.MapGet("/locks/release/{key}/{machineName}/{ownerId}",
            async (IDataLockDbContext dataLockDbContext, string key, string machineName, string ownerId) =>
                await dataLockDbContext.ReleaseLock(key, machineName, ownerId));
    }
}
using DTO;
using ILockDataAccess;
using LockDataAccess;

/// <summary>
///     Represents the entry point for the program.
/// </summary>
public class Program
{
    /// <summary>
    ///     This method is the entry point for the application.
    /// </summary>
    /// <param name="args">Command-line arguments passed to the application.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
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

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapGet("/locks", async (IDataLockDbContext dataLockDbContext) => Results.Ok(await dataLockDbContext.GetLocks()));

        app.MapGet("/locks/{key}",
            async (IDataLockDbContext dataLockDbContext, string key) => Results.Ok(await dataLockDbContext.GetLock(key)));

        app.MapGet("/locks/{key}/{machineName}/{ownerId}",
            async (IDataLockDbContext dataLockDbContext, string key, string machineName, string ownerId) =>
                await dataLockDbContext.AcquireLock(key, machineName, ownerId));

        app.MapGet("/locks/release/{key}/{machineName}/{ownerId}",
            async (IDataLockDbContext dataLockDbContext, string key, string machineName, string ownerId) =>
                await dataLockDbContext.ReleaseLock(key, machineName, ownerId));

        app.Run();
    }
}

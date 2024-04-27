namespace DistributedLock;

using DTO;

public class Program
{
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

        var config = configuration.GetSection(nameof(DataAccess)).Get<DataAccess>();
        var connectionString = $"{config?.ConnectionString};User Id={config?.UserId};Password={config?.Password}";

        // Add Entity Framework DbContext with SQL Server provider
        // services.AddDbContext<MailDbContext>(options => { options.UseSqlServer(connectionString); }, ServiceLifetime.Singleton)
        //    .BuildServiceProvider();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Run();
    }
}

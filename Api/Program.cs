using Api.Endpoints;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api
{
    /// <summary>
    /// Because there are only two endpoints and the requirements for the API itself are fairly straight-forward, we're using a Minimal API.
    /// Builds a service collection and routes requests to the MovieService via Api.Endpoints.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = BuildApp(args);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();
            context.Database.Migrate();

            var movieEndpoints = scope.ServiceProvider.GetRequiredService<MovieEndpoints>();
            app.MapGet("/movies", async () => await movieEndpoints.ListAll()).WithOpenApi();
            app.MapGet("/movies/query", async (MovieQuery queryRequest) => await movieEndpoints.Query(queryRequest)).WithOpenApi();

            app.Run();
        }

        private static WebApplication BuildApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("appsettings.json", false, true);

            builder.Logging.ClearProviders();
            var logger = new LoggerConfiguration()
            .WriteTo.File($"{Directory.GetCurrentDirectory()}/logging/log.log", Serilog.Events.LogEventLevel.Warning)
            .CreateLogger();
            builder.Host.UseSerilog(logger);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Data Source={builder.Configuration["Sqlite3DbPath"]}"));
            builder.Services.AddMemoryCache();

            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<MovieEndpoints, MovieEndpoints>();

            return builder.Build();
        }
    }
}
namespace Api
{
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

            var movieService = scope.ServiceProvider.GetRequiredService<IMovieService>();
            app.MapGet("/movies", movieService.ListAll).WithOpenApi();
            app.MapGet("/movies/query", movieService.Query).WithOpenApi();

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

            return builder.Build();
        }
    }
}


namespace Api {
    public class Program{
        public static void Main(string[] args){
            var app = BuildApp(args);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope()){
                var movieService = scope.ServiceProvider.GetRequiredService<IMovieService>();

                app.MapGet("/movies", async () => await movieService.ListAll()).WithOpenApi();
                app.MapGet("/movies/query", async (HttpRequest request) => {
                    int.TryParse(request.Query["from"], out var from);
                    int.TryParse(request.Query["to"], out var to);
                    int.TryParse(request.Query["pageSize"], out var pageSize);
                    int.TryParse(request.Query["pageNumber"], out var pageNumber);
                    var searchPhrase = request.Query["searchPhrase"];
                    
                    var res = await movieService.Query(from: from, to: to, pageSize: pageSize, pageNumber: pageNumber, searchPhrase: searchPhrase);

                    return res;
                }).WithOpenApi();
            };
    
            app.Run();
        }

        private static WebApplication BuildApp(string[] args){
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Configuration.AddJsonFile("appsettings.json", false, true);
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Data Source={builder.Configuration["Sqlite3DbPath"]}"));

            builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
            builder.Services.AddScoped<IGenericRepository<Movie>, GenericRepository<Movie>>();
            builder.Services.AddScoped<IMovieService, MovieService>();

            return builder.Build();
        }
    }
}



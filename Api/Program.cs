namespace Api {
    public class Program{
        public void Main(string[] args){
            var app = BuildApp(args);
            MovieEndpoints.Map(app);

            app.Run();
        }

        private WebApplication BuildApp(string[] args){
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.json", false, true);
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Data Source={builder.Configuration["Sqlite3DbPath"]}"));

            builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
            builder.Services.AddScoped<IMovieService, MovieService>();

            return builder.Build();
        }
    }
}



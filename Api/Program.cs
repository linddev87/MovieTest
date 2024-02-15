
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Data Source={builder.Configuration["Sqlite3DbPath"]}"));
            
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

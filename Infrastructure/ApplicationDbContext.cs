namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public ApplicationDbContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasAlternateKey(m => m.AlternateKey);

            modelBuilder.Entity<Movie>().HasIndex(m => m.AlternateKey).IsUnique(true);
            modelBuilder.Entity<Movie>().HasIndex(m => m.Year).IsDescending(true);
            modelBuilder.Entity<Movie>().HasIndex(m => m.TitleToLower);
        }
    }
}

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasAlternateKey(m => new {m.Year, m.Name});
        }
    }
}

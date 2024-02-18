namespace Infrastructure
{
    public class DesignTimeDbContext : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite($"Data Source={args[0]}");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
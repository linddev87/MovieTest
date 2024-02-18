namespace Tests
{
    internal class TestApplicationDbContext : ApplicationDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseInMemoryDatabase("TestDb");
        }
    }
}
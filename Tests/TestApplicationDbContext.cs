namespace Tests
{
    internal class TestApplicationDbContext : ApplicationDbContext
    {
        private readonly string _dbName;

        public TestApplicationDbContext()
        {
            _dbName = Guid.NewGuid().ToString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseInMemoryDatabase(_dbName);
        }
    }
}
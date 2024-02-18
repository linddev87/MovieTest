namespace Tests
{
    internal class TestApplicationDbContext : ApplicationDbContext
    {
        private readonly string _dbName;

        public TestApplicationDbContext(string dbName)
        {
            _dbName = dbName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseInMemoryDatabase(_dbName);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class TestApplicationDbContext : ApplicationDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder) { 
            builder.UseInMemoryDatabase("TestDb");
        } 
    }
}

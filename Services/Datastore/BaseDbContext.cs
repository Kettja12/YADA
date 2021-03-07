using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Datastore
{
    public class BaseDbContext:DbContext
    {
        public BaseDbContext(string connectionstring) : base(StaticLib.GetOptions(connectionstring))
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
            .LogTo(Console.WriteLine,LogLevel.Information)
            .EnableSensitiveDataLogging()
            .UseSqlServer();
    }
}

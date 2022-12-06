using Microsoft.EntityFrameworkCore;
using NewsApp.Models;

namespace NewsApp.Data
{
    public class DataContext: DbContext
    {
        internal readonly Task NewsModels;

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<NewsModels> News { get; set; }
    }
}

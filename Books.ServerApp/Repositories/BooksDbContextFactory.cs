using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Books.ServerApp.Repositories
{
    public class BooksDbContextFactory : IDesignTimeDbContextFactory<BooksDbContext>
    {
        public BooksDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BooksDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\Server;Database=Database;Trusted_Connection=True");

            return new BooksDbContext(optionsBuilder.Options);
        }
    }
}
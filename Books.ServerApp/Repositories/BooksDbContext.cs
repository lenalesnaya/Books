using Books.ServerApp.DomainModel;
using Books.ServerApp.Repositories.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Books.ServerApp.Repositories
{
    public class BooksDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }


        public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BooksConfiguration).Assembly);
        }
    }
}
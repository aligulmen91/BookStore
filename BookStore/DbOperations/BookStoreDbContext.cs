using Microsoft.EntityFrameworkCore;

namespace BookStore.DbOperations
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) :base(options)
        {

        }

        //we create dbset of books
        public DbSet<Book> Books { get; set; }
    }
}

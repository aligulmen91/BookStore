using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BookStore.DbOperations
{
    public class DataGenerator
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            { //if there is no record on dbcontext, add new sample datas to work with.
                if (context.Books.Any())
                {
                    return;
                }

                context.Books.AddRange(

                new Book {  /*Id = 1, */ Title = "Lean Startup", GenreId = 1, PageCount = 200, PublishDate = new DateTime(2015, 06, 11) },
                new Book { /*Id = 2,*/ Title = "Herland", GenreId = 2, PageCount = 250, PublishDate = new DateTime(2001, 02, 10) },
                new Book { /* Id = 3, */ Title = "Dune", GenreId = 2, PageCount = 250, PublishDate = new DateTime(2001, 02, 10) }

                );
                context.SaveChanges();

            }
        }
    }
}

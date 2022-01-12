using BookStore.Common;
using BookStore.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        //first we define private readonly. to change only on constructor
        private readonly BookStoreDbContext _dbContext;
        public GetBooksQuery(BookStoreDbContext dbContext)
        {
            //and get all datas from query. we will use readonly prop in here
            _dbContext = dbContext;
        }





        //in here we will collect data from tables to view model, in these way, we can show whatever we need on return
        public List<BooksViewModel> Handle()
        {
            
            var bookList = _dbContext.Books.OrderBy(b => b.Id).ToList<Book>();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach (var book in bookList)
            {
                vm.Add(new BooksViewModel()
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"),
                   PageCount = book.PageCount
                }); 
            }
            return vm;
        }


        //this is our viewmodel. 
        public class BooksViewModel
        {
            public string Title { get; set; }
            public string Genre { get; set; } //we change it to string, because it is view, we will show the name of genre
            public int PageCount { get; set; }
            public string PublishDate { get; set; } //we will use it as a string


        }
    }
}

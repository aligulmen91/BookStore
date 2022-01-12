using BookStore.DbOperations;
using System;
using System.Linq;

namespace BookStore.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }

        private readonly BookStoreDbContext _dbContext;
        public CreateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        // we will get a model from user and set it to context
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b => b.Title == Model.Title); //check if we already have that book
            if (book is not null)
                throw new InvalidOperationException("Book is already exist!");
            book = new Book();
            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.PageCount = Model.PageCount;
            book.GenreId = Model.GenreId;

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }


        //this is what we get from user
        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}

using AutoMapper;
using BookStore.Common;
using BookStore.DbOperations;
using System;
using System.Linq;

namespace BookStore.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; } //the id which will come from outside
  
        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }



        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(b => b.Id == BookId).SingleOrDefault();
            if (book is null)
                throw new InvalidOperationException("The book is not exist!");

            var vm = _mapper.Map<BookDetailViewModel>(book);

         
            return vm;
        }


        public class BookDetailViewModel
        {
            public string Title { get; set; }
            public string Genre { get; set; } //we change it to string, because it is view, we will show the name of genre
            public int PageCount { get; set; }
            public string PublishDate { get; set; } //we will use it as a string

        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DbOperations;

namespace BookStore.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        //private static List<Book> BookList = new List<Book>()
        //{
        //    //when we don't use database, we can define a list to work on it
        //    new Book{Id=1,Title="Lean Startup",GenreId=1,PageCount=200,PublishDate= new DateTime(2015,06,11)},
        //    new Book{Id=2,Title="Herland",GenreId=2,PageCount=250,PublishDate= new DateTime(2001,02,10)},
        //    new Book{Id=3,Title="Dune",GenreId=2,PageCount=250,PublishDate= new DateTime(2001,02,10)}
        //};



        //create end points - use LINQ

        //api/Books
        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(b => b.Id).ToList<Book>();
            return bookList;
        }



        //api/Books/3
        [HttpGet("{id}")]
        public Book GetBooksById(int id)
        {
            var book = _context.Books.Where(b=>b.Id == id).SingleOrDefault();
            return book;
        }


        //api/Books?id=3
        //[HttpGet]
        //public Book Get([FromQuery]string id)
        //{
        //    var book = BookList.Where(b => b.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}

        //add new books api/Books
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(b => b.Title == newBook.Title); //check if we already have that book
            if (book is not null)
                return BadRequest();
            
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok();
        }


        //update a book  api/Books
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updateBook)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id); //at first, find the book to update
            if (book is null)
                return BadRequest();
            //if the value is not default, it means user already tried to update it. We can use input value. Otherwise, use recorded value and don't change it
            book.GenreId = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId; 
            book.Title = updateBook.Title != default ? updateBook.Title : book.Title; 
            book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount; 
            book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            _context.SaveChanges(); //every time we make some changes on dbcontext, don't forget to savechanges.
            return Ok();
        }

        //api/Books/3
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id); //is it exist?
            if(book is null)
                return BadRequest();
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }

    }

}

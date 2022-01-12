using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DbOperations;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.CreateBook;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;

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


        //api/Books
        [HttpGet]
        public IActionResult GetBooks()
        {         
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);

        }


        //add new books api/Books
        [HttpPost]
        public IActionResult AddBook([FromBody] CreteBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {        
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
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

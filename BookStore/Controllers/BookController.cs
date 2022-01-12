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
using BookStore.BookOperations.GetBookDetail;
using static BookStore.BookOperations.GetBookDetail.GetBookDetailQuery;
using BookStore.BookOperations.UpdateBook;
using static BookStore.BookOperations.UpdateBook.UpdateBookCommand;
using BookStore.BookOperations.DeleteBook;

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
            var query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);

        }


        //add new books api/Books
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            var command = new CreateBookCommand(_context);
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
        public IActionResult GetBooksById(int id)
        {
            BookDetailViewModel result;
            try
            {
                var query = new GetBookDetailQuery(_context);
                query.BookId = id;
                result= query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok(result);
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
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updateBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }



        //api/Books/3
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                var command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

    }

}

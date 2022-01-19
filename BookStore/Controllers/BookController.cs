using Microsoft.AspNetCore.Mvc;
using System;
using BookStore.DbOperations;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.CreateBook;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using BookStore.BookOperations.GetBookDetail;
using static BookStore.BookOperations.GetBookDetail.GetBookDetailQuery;
using BookStore.BookOperations.UpdateBook;
using static BookStore.BookOperations.UpdateBook.UpdateBookCommand;
using BookStore.BookOperations.DeleteBook;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;

namespace BookStore.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var command = new CreateBookCommand(_context, _mapper);

            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();


            return Ok();
        }




        //api/Books/3
        [HttpGet("{id}")]
        public IActionResult GetBooksById(int id)
        {
            BookDetailViewModel result;

            var query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;


            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);

            result = query.Handle();

            return Ok(result);
        }



        //update a book  api/Books
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updateBook;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }



        //api/Books/3
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {

            var command = new DeleteBookCommand(_context);
            command.BookId = id;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);


            command.Handle();

            return Ok();
        }

    }

}

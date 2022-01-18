using FluentValidation;
using System;

namespace BookStore.BookOperations.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(0); //command = CreateBookCommand
            RuleFor(command => command.Model.PageCount).GreaterThan(0); //command = CreateBookCommand
            RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThanOrEqualTo(DateTime.Now.Date);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4); 

        }
    }
}

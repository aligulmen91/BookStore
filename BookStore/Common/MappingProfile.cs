using AutoMapper;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;

namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //hangi entity neye dönüşebilir tanımlayacağız.
            CreateMap<CreateBookModel, Book>();
        }

    }
}

using AutoMapper;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.BookOperations.GetBookDetail.GetBookDetailQuery;

namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<CreateBookModel, Book>();
           
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(dest =>dest.Genre,               //formember : for each member. destination is genre
                opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString())); //map : cast genreenum to string
        }

    }
}

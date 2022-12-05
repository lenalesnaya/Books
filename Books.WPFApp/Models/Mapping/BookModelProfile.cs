using Books.ServerApp.DomainModel;
using Books.WPFApp.Models.BooksModels;
using AutoMapper;

namespace Books.WPFApp.Models.Mapping
{
    public class BookModelProfile : Profile
    {
        public BookModelProfile()
        {
            CreateMap<Book, BookModel>();
            CreateMap<BookModel, Book>();
        }
    }
}
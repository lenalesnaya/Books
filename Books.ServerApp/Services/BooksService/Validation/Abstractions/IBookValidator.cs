using Books.ServerApp.DomainModel;
using Books.ServerApp.Services.BooksService.Abstractions;
using App.Common.Validation.Abstractions;

namespace Books.ServerApp.Services.BooksService.Validation.Abstractions
{
    public interface IBookValidator : IValidator<Book, BookErrors>
    {
    }
}
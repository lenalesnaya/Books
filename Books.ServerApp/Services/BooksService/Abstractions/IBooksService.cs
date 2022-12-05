using App.Common;
using Books.ServerApp.DomainModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.ServerApp.Services.BooksService.Abstractions
{
    public interface IBooksService
    {
        Task<IList<Book>> GetAllBooksAsync();

        Task<IList<Book>> GetBooksRangeAsync(Range range);

        Task<IList<Book>> GetBooksByTitleAsync(string title);

        Task<IList<Book>> GetBooksByAuthorsSurnameOrPenNameAsync(string title);

        Task<Book> GetBookByIdAsync(int id);

        Task<OperationResult<Book, BookErrors>> AddBookAsync(Book newBook, IFormFile uploadedImageFile);

        Task<OperationResult<BookErrors>> UpdateBookAsync(Book book, bool isImageDeleting, IFormFile uploadedImageFile);

        Task<OperationResult<BookErrors>> RemoveBookAsync(Book book);

        Task<OperationResult<BookErrors>> RemoveBookByIdAsync(int id);
    }
}
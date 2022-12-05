using Books.ServerApp.DomainModel;
using Books.ServerApp.Services.BooksService.Abstractions;
using Books.ServerApp.Services.BooksService.Validation.Abstractions;
using App.Common.Repositories.Abstractions;
using App.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Books.ServerApp.Services.BooksService.Configuration;
using System.Linq;

namespace Books.ServerApp.Services.BooksService
{
    public class BooksService : IBooksService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookValidator _bookValidator;


        public BooksService(IUnitOfWork unitOfWork, IBookValidator bookValidator)
        {
            _unitOfWork = unitOfWork;
            _bookValidator = bookValidator;
        }

        public async Task<IList<Book>> GetAllBooksAsync()
        {
            var repository = _unitOfWork.GetRepository<Book>();
            var books = await repository.GetAllAsync();

            return books.ToList();
        }

        public async Task<IList<Book>> GetBooksRangeAsync(Range range)
        {
            var repository = _unitOfWork.GetRepository<Book>();
            var booksCollection = await repository.GetAllAsync();
            var booksRange = booksCollection.ToArray()[range];

            return booksRange.ToList();
        }

        public async Task<IList<Book>> GetBooksByTitleAsync(string title)
        {
            var repository = _unitOfWork.GetRepository<Book>();
            var books = await repository.GetWhereAsync(book => book.Title.Equals(title));

            return books.ToList();
        }

        public async Task<IList<Book>> GetBooksByAuthorsSurnameOrPenNameAsync(string surnameOrPenName)
        {
            var repository = _unitOfWork.GetRepository<Book>();
            var books = await repository.GetWhereAsync(book => book.AuthorsSurnameOrPenName.Equals(surnameOrPenName));

            return books.ToList();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var repository = _unitOfWork.GetRepository<Book>();

            return await repository.GetSingleOrDefaultAsync(book => book.Id == id);
        }

        public async Task<OperationResult<Book, BookErrors>> AddBookAsync(Book newBook, IFormFile uploadedImageFile)
        {
            var validationResult = _bookValidator.Validate(newBook);

            if (!validationResult.IsSuccessful) 
                return OperationResult<Book, BookErrors>.CreateUnsuccessful(validationResult.Error.GetValueOrDefault());

            try
            {
                await SetImage(uploadedImageFile, newBook);

                var repository = _unitOfWork.GetRepository<Book>();
                repository.Add(newBook);
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                return OperationResult<Book, BookErrors>.CreateUnsuccessful(BookErrors.BookCreationIsFailed);
            }

            return OperationResult<Book, BookErrors>.CreateSuccessful(newBook);
        }

        public async Task<OperationResult<BookErrors>> UpdateBookAsync(Book book, bool isImageDeleting, IFormFile uploadedImageFile)
        {
            var validationResult = _bookValidator.Validate(book);

            if (!validationResult.IsSuccessful)
                return OperationResult<BookErrors>.CreateUnsuccessful(validationResult.Error.GetValueOrDefault());

            try
            {
                if (isImageDeleting)
                    DeleteImage(book);
                else await SetImage(uploadedImageFile, book);

                var repository = _unitOfWork.GetRepository<Book>();
                repository.Update(book);
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                return OperationResult<BookErrors>.CreateUnsuccessful(BookErrors.BookUpdatingIsFailed);
            }

            return OperationResult<BookErrors>.CreateSuccessful();
        }

        public async Task<OperationResult<BookErrors>> RemoveBookAsync(Book book)
        {
            try
            {
                DeleteImageFromDisk(book.ImageFilePath);

                var repository = _unitOfWork.GetRepository<Book>();
                repository.Remove(book);
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                return OperationResult<BookErrors>.CreateUnsuccessful(BookErrors.BookDeletingIsFailed);
            }

            return OperationResult<BookErrors>.CreateSuccessful();
        }

        public async Task<OperationResult<BookErrors>> RemoveBookByIdAsync(int id)
        {
            var repository = _unitOfWork.GetRepository<Book>();
            var book = await repository.GetSingleOrDefaultAsync(book => book.Id == id);

            try
            {
                repository.Remove(book);
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                return OperationResult<BookErrors>.CreateUnsuccessful(BookErrors.BookDeletingIsFailed);
            }

            return OperationResult<BookErrors>.CreateSuccessful();
        }


        private async Task<OperationResult<Book, BookErrors>> SetImage(IFormFile uploadedImageFile, Book book)
        {
            if (uploadedImageFile != null)
            {
                DeleteImageFromDisk(book.ImageFilePath);
                var imagePath = CreateImagePath(uploadedImageFile, book.Id);
                await WriteImageOnDisk(uploadedImageFile, imagePath);

                book.ImageFilePath = imagePath;
            }

            return OperationResult<Book, BookErrors>.CreateSuccessful(book);
        }

        private OperationResult<Book, BookErrors> DeleteImage(Book book)
        {
            DeleteImageFromDisk(book.ImageFilePath);

            book.ImageFilePath = null;

            return OperationResult<Book, BookErrors>.CreateSuccessful(book);
        }


        private void DeleteImageFromDisk(string imageFilePath)
        {
            if (imageFilePath != null)
            {
                FileInfo fileInfo = new FileInfo(imageFilePath);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
            }
        }

        private async Task WriteImageOnDisk(IFormFile uploadedImageFile, string imagePath)
        {
            using var fileStream = new FileStream(imagePath, FileMode.Create);
            await uploadedImageFile.CopyToAsync(fileStream);
        }

        private string CreateImagePath(IFormFile uploadedImageFile, int bookId)
        {
            string path = BookConstants.ImagesFolderPath +
                bookId +
                "image" +
                uploadedImageFile.GetHashCode() +
                uploadedImageFile.FileName;

            return path;
        }
    }
}
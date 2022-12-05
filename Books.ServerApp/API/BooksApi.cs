using Books.ServerApp.DomainModel;
using Books.ServerApp.Services.BooksService.Abstractions;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Books.ServerApp.API
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BooksApi : ControllerBase
    {
        private readonly IBooksService _booksService;


        public BooksApi(IBooksService booksService)
        {
            _booksService = booksService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            try
            {
                var books = await _booksService.GetAllBooksAsync();

                return Ok(books);
            }
            catch
            {
                var errorMessage = GetErrorMessage(BookErrors.UnknownError);

                return BadRequest(errorMessage);
            }
        }

        [Route("{upperBound}/{lowerBound}")]
        [HttpGet]
        public async Task<IActionResult> GetBooksRangeAsync(int upperBound, int lowerBound)
        {
            var range = new Range (upperBound, lowerBound);
            try
            {
                var books = await _booksService.GetBooksRangeAsync(range);

                return Ok(books);
            }
            catch
            {
                var errorMessage = GetErrorMessage(BookErrors.UnknownError);

                return BadRequest(errorMessage);
            }
        }

        [Route("{title}")]
        [HttpGet]
        public async Task<IActionResult> GetBooksByTitleAsync(string title)
        {
            try
            {
                var books = await _booksService.GetBooksByTitleAsync(title);

                return Ok(books);
            }
            catch
            {
                var errorMessage = GetErrorMessage(BookErrors.UnknownError);

                return BadRequest(errorMessage);
            }
        }

        [Route("{surnameOrPenName}")]
        [HttpGet]
        public async Task<IActionResult> GetBooksByAuthorsSurnameOrPenNameAsync(string surnameOrPenName)
        {
            try
            {
                var books = await _booksService.GetBooksByAuthorsSurnameOrPenNameAsync(surnameOrPenName);

                return Ok(books);
            }
            catch
            {
                var errorMessage = GetErrorMessage(BookErrors.UnknownError);

                return BadRequest(errorMessage);
            }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetBookByIdAsync(int id)
        {
            try
            {
                var book = await _booksService.GetBookByIdAsync(id);

                return Ok(book);
            }
            catch
            {
                var errorMessage = GetErrorMessage(BookErrors.UnknownError);

                return BadRequest(errorMessage);
            }
        }

        [Route("{newBook}/{uploadedImageFile}")]
        [HttpPost]
        public async Task<IActionResult> AddBookAsync(Book newBook, IFormFile uploadedImageFile)
        {
            var result = await _booksService.AddBookAsync(newBook, uploadedImageFile);
            if (result.IsSuccessful)
            {
                return Ok("The book is added.");
            }

            var errorMessage = GetErrorMessage(result.Error.GetValueOrDefault());

            return BadRequest(errorMessage);

        }

        [Route("{book}/{isImageDeleting}/{uploadedImageFile}")]
        [HttpPut]
        public async Task<IActionResult> UpdateBookAsync(Book book, bool isImageDeleting, IFormFile uploadedImageFile)
        {
            var result = await _booksService.UpdateBookAsync(book, isImageDeleting, uploadedImageFile);
            if (result.IsSuccessful)
            {
                return Ok("The book is updated.");
            }

            var errorMessage = GetErrorMessage(result.Error.GetValueOrDefault());

            return BadRequest(errorMessage);
        }


        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBookByIdAsync(int id)
        {
            var result = await _booksService.RemoveBookByIdAsync(id);
            if (result.IsSuccessful)
            {
                return Ok("The book is deleted.");
            }

            var errorMessage = GetErrorMessage(result.Error.GetValueOrDefault());

            return BadRequest(errorMessage);
        }


        private static string GetErrorMessage(BookErrors error)
        {
            var errorMessage = error switch
            {
                BookErrors.TitleIsRequired => "A title is required",
                BookErrors.InvalidTitleLength => "A title must not exceed 128 characters",
                BookErrors.InvalidAuthorsSurnameOrPenNameLength => "A surname or pen name must not exceed 128 characters",
                BookErrors.InvalidAuthorsNameLength => "A name must not exceed 128 characters",
                BookErrors.InvalidCommentLength => "A comment must not exceed 500 characters",
                BookErrors.BookCreationIsFailed => "Book adding is failed",
                BookErrors.BookUpdatingIsFailed => "Book updating is failed",
                BookErrors.BookDeletingIsFailed => "Book deleting is failed",
                BookErrors.UnknownError => "Access error",
                _ => throw new ArgumentOutOfRangeException(
                    $"The value passed as an argument \"{nameof(error)}\" ({error}) is not valid for the method.")
            };

            return errorMessage;
        }
    }
}
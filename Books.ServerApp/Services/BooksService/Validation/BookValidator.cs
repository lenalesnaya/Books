using Books.ServerApp.DomainModel;
using Books.ServerApp.Services.BooksService.Abstractions;
using Books.ServerApp.Services.BooksService.Configuration;
using Books.ServerApp.Services.BooksService.Validation.Abstractions;
using App.Common.Validation.Abstractions;

namespace Books.ServerApp.Services.BooksService.Validation
{
    public class BookValidator : Validator<Book, BookErrors>, IBookValidator
    {
        protected override BookErrors? HandleValidate(Book book)
        {
            var titleError = ValidateTitle(book.Title);
            if (titleError != null)
            {
                return titleError;
            }

            var authorsSurnameOrPenNameError = ValidateAuthorsSurnameOrPenName(book.AuthorsSurnameOrPenName);
            if (authorsSurnameOrPenNameError != null)
            {
                return authorsSurnameOrPenNameError;
            }

            var authorsNameError = ValidateAuthorsName(book.AuthorsName);
            if (authorsNameError != null)
            {
                return authorsNameError;
            }

            var commentError = ValidateComment(book.Comment);

            return commentError;
        }


        private BookErrors? ValidateTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BookErrors.TitleIsRequired;
            }

            var isTitleLengthValid = BookConstants.TitleMaxLength >= title.Length;
            if (!isTitleLengthValid)
            {
                return BookErrors.InvalidTitleLength;
            }

            return null;
        }

        private BookErrors? ValidateAuthorsSurnameOrPenName(string surnameOrPenName)
        {
            if(!string.IsNullOrEmpty(surnameOrPenName))
            {
                var isSurnameOrPenNameLengthValid = BookConstants.AuthorsSurnameOrPenNameMaxLength >= surnameOrPenName.Length;
                if (!isSurnameOrPenNameLengthValid)
                {
                    return BookErrors.InvalidAuthorsSurnameOrPenNameLength;
                }
            }

            return null;
        }

        private BookErrors? ValidateAuthorsName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var isNameLengthValid = BookConstants.AuthorsNameMaxLength >= name.Length;
                if (!isNameLengthValid)
                {
                    return BookErrors.InvalidAuthorsNameLength;
                }
            }

            return null;
        }

        private BookErrors? ValidateComment(string comment)
        {
            if (!string.IsNullOrEmpty(comment))
            {
                var isCommentLengthValid = BookConstants.CommentMaxLength >= comment.Length;
                if (!isCommentLengthValid)
                {
                    return BookErrors.InvalidCommentLength;
                }
            }

            return null;
        }
    }
}
namespace Books.ServerApp.Services.BooksService.Abstractions
{
    public enum BookErrors
    {
        TitleIsRequired,
        InvalidTitleLength,
        InvalidAuthorsSurnameOrPenNameLength,
        InvalidAuthorsNameLength,
        InvalidCommentLength,
        BookCreationIsFailed,
        BookUpdatingIsFailed,
        BookDeletingIsFailed,
        UnknownError
    }
}
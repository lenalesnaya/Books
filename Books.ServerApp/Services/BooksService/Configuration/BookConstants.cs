namespace Books.ServerApp.Services.BooksService.Configuration
{
    public static class BookConstants
    {
        public const int TitleMaxLength = 128;
        public const int AuthorsSurnameOrPenNameMaxLength = 128;
        public const int AuthorsNameMaxLength = 128;
        public const int CommentMaxLength = 500;

        public const string DefaultImageFolderPath = @"\images\";
        public const string DefaultImageFileName = "DefaultImage.png";
        public const string ImagesFolderPath = @"\images\userimages\";
    }
}
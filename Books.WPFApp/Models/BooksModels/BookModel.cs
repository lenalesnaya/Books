namespace Books.WPFApp.Models.BooksModels
{
    public class BookModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorsSurnameOrPenName { get; set; }

        public string AuthorsName { get; set; }

        public string ImageFilePath { get; set; }

        public string Comment { get; set; }

        public bool WannaRead { get; set; }

        public bool HaveRead
        {
            get { return HaveRead; }
            set
            {
                HaveRead = value;
                if (HaveRead == false)
                    IsFavorite = false;
            }
        }

        public bool IsFavorite
        {
            get { return IsFavorite; }
            set
            {
                if (HaveRead == false)
                    IsFavorite = false;
                else IsFavorite = value;
            }
        }
    }
}
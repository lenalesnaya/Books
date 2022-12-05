using Books.ServerApp.DomainModel;
using Books.ServerApp.Services.BooksService.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.ServerApp.Repositories.Configurations
{
    internal class BooksConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .Property(b => b.Title)
                .HasMaxLength(BookConstants.TitleMaxLength)
                .IsRequired();
            builder
                .Property(b => b.AuthorsSurnameOrPenName)
                .HasMaxLength(BookConstants.AuthorsSurnameOrPenNameMaxLength);
            builder
                .Property(b => b.AuthorsName)
                .HasMaxLength(BookConstants.AuthorsNameMaxLength);
            builder
                .Property(b => b.Comment)
                .HasMaxLength(BookConstants.CommentMaxLength);
        }
    }
}
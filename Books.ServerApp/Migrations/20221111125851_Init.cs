using Microsoft.EntityFrameworkCore.Migrations;

namespace Books.ServerApp.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    AuthorsSurnameOrPenName = table.Column<string>(maxLength: 128, nullable: true),
                    AuthorsName = table.Column<string>(maxLength: 128, nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(maxLength: 500, nullable: true),
                    WannaRead = table.Column<bool>(nullable: false),
                    HaveRead = table.Column<bool>(nullable: false),
                    IsFavorite = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}

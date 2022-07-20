using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShortUrl.Persistence.Migrations
{
    public partial class IntroduceShortUrlItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShortUrlItems",
                columns: table => new
                {
                    Shortcut = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortUrlItems", x => x.Shortcut);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShortUrlItems_Url",
                table: "ShortUrlItems",
                column: "Url",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShortUrlItems");
        }
    }
}

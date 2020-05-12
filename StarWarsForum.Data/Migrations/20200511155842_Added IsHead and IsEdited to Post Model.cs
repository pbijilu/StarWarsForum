using Microsoft.EntityFrameworkCore.Migrations;

namespace StarWarsForum.Data.Migrations
{
    public partial class AddedIsHeadandIsEditedtoPostModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEdited",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHead",
                table: "Posts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEdited",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsHead",
                table: "Posts");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace ReactRandomJokes.Data.Migrations
{
    public partial class joke : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JokeText",
                table: "Jokes",
                newName: "Setup");

            migrationBuilder.AddColumn<string>(
                name: "Punchline",
                table: "Jokes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Punchline",
                table: "Jokes");

            migrationBuilder.RenameColumn(
                name: "Setup",
                table: "Jokes",
                newName: "JokeText");
        }
    }
}

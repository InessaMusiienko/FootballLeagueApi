using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballLeagueApi.Migrations
{
    public partial class UpdateAndAddColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "Result",
                table: "Matches",
                newName: "Winner");

            migrationBuilder.RenameColumn(
                name: "IsVisible",
                table: "Matches",
                newName: "IsPlayed");

            migrationBuilder.AddColumn<string>(
                name: "Team1",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Team2",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Team1",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Team2",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "Winner",
                table: "Matches",
                newName: "Result");

            migrationBuilder.RenameColumn(
                name: "IsPlayed",
                table: "Matches",
                newName: "IsVisible");

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Teams",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballLeagueApi.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPoint",
                table: "Teams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalPoint",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class fixQuestionaries1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Headache",
                table: "Questionaire",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ObstructedBreathing",
                table: "Questionaire",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Tiredness",
                table: "Questionaire",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Headache",
                table: "Questionaire");

            migrationBuilder.DropColumn(
                name: "ObstructedBreathing",
                table: "Questionaire");

            migrationBuilder.DropColumn(
                name: "Tiredness",
                table: "Questionaire");
        }
    }
}

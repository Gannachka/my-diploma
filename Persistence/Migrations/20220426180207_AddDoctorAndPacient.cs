using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddDoctorAndPacient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questionaire_Users_UserId",
                table: "Questionaire");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "temperature",
                table: "Questionaire",
                newName: "Temperature");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Questionaire",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Questionaire",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Questionaire",
                newName: "PacientId");

            migrationBuilder.RenameIndex(
                name: "IX_Questionaire_UserId",
                table: "Questionaire",
                newName: "IX_Questionaire_PacientId");

            migrationBuilder.AlterColumn<int>(
                name: "PacientId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkExperience = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorId);
                });

            migrationBuilder.CreateTable(
                name: "Pacients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Pacients_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DoctorId",
                table: "Users",
                column: "DoctorId",
                unique: true,
                filter: "[DoctorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PacientId",
                table: "Users",
                column: "PacientId",
                unique: true,
                filter: "[PacientId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Pacients_DoctorId",
                table: "Pacients",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questionaire_Pacients_PacientId",
                table: "Questionaire",
                column: "PacientId",
                principalTable: "Pacients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Doctors_DoctorId",
                table: "Users",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Pacients_PacientId",
                table: "Users",
                column: "PacientId",
                principalTable: "Pacients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questionaire_Pacients_PacientId",
                table: "Questionaire");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Doctors_DoctorId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Pacients_PacientId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Pacients");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Users_DoctorId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PacientId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Temperature",
                table: "Questionaire",
                newName: "temperature");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Questionaire",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Questionaire",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PacientId",
                table: "Questionaire",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Questionaire_PacientId",
                table: "Questionaire",
                newName: "IX_Questionaire_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "PacientId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Questionaire_Users_UserId",
                table: "Questionaire",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

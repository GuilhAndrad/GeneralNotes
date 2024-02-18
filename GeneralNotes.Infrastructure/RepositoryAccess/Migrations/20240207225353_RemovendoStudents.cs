using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeneralNotes.Infrastructure.RepositoryAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutRoutines_Sudents_StudentId",
                table: "WorkoutRoutines");

            migrationBuilder.DropTable(
                name: "Sudents");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutRoutines_StudentId",
                table: "WorkoutRoutines");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "WorkoutRoutines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StudentId",
                table: "WorkoutRoutines",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Sudents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Contact = table.Column<string>(type: "varchar(14)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Height = table.Column<double>(type: "REAL", nullable: false),
                    MedicalHistory = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sudents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutRoutines_StudentId",
                table: "WorkoutRoutines",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutRoutines_Sudents_StudentId",
                table: "WorkoutRoutines",
                column: "StudentId",
                principalTable: "Sudents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

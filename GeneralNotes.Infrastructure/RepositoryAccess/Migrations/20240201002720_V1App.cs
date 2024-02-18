using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeneralNotes.Infrastructure.RepositoryAccess.Migrations
{
    /// <inheritdoc />
    public partial class V1App : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sudents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Contact = table.Column<string>(type: "varchar(14)", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    Height = table.Column<double>(type: "REAL", nullable: false),
                    MedicalHistory = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sudents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Contact = table.Column<string>(type: "varchar(14)", nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutRoutines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutRoutines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutRoutines_Sudents_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Sudents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Type = table.Column<string>(type: "varchar(50)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Charge = table.Column<double>(type: "double", nullable: false),
                    Repetitions = table.Column<int>(type: "int", nullable: false),
                    Stop = table.Column<string>(type: "int", nullable: false),
                    Equipment = table.Column<string>(type: "varchar(100)", nullable: false),
                    Details = table.Column<string>(type: "varchar(350)", nullable: false),
                    WorkoutRoutineId = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_WorkoutRoutines_WorkoutRoutineId",
                        column: x => x.WorkoutRoutineId,
                        principalTable: "WorkoutRoutines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutRoutineId",
                table: "Exercises",
                column: "WorkoutRoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutRoutines_StudentId",
                table: "WorkoutRoutines",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkoutRoutines");

            migrationBuilder.DropTable(
                name: "Sudents");
        }
    }
}

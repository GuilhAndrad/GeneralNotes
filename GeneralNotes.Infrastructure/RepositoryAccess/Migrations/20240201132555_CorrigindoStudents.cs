using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeneralNotes.Infrastructure.RepositoryAccess.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "WorkoutRoutines",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorkoutRoutines");
        }
    }
}

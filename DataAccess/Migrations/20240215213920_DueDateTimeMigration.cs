using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DueDateTimeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "ListTasks");

            migrationBuilder.DropColumn(
                name: "DueTime",
                table: "ListTasks");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDateTime",
                table: "ListTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDateTime",
                table: "ListTasks");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DueDate",
                table: "ListTasks",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "DueTime",
                table: "ListTasks",
                type: "time",
                nullable: true);
        }
    }
}

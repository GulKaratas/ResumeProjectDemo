using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeProjectDemo.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSkilsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Skils1",
                table: "Skilss");

            migrationBuilder.DropColumn(
                name: "Skils2",
                table: "Skilss");

            migrationBuilder.DropColumn(
                name: "Skils3",
                table: "Skilss");

            migrationBuilder.DropColumn(
                name: "Skils4",
                table: "Skilss");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Skilss",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Percentage",
                table: "Skilss",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Skilss");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "Skilss");

            migrationBuilder.AddColumn<string>(
                name: "Skils1",
                table: "Skilss",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Skils2",
                table: "Skilss",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Skils3",
                table: "Skilss",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Skils4",
                table: "Skilss",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

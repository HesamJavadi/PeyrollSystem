using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollSystem.Infrastructure.SQL.Migrations
{
    /// <inheritdoc />
    public partial class addDashboadDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DashboadDescription",
                schema: "ps",
                table: "Setting",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DashboadDescription",
                schema: "ps",
                table: "Setting");
        }
    }
}

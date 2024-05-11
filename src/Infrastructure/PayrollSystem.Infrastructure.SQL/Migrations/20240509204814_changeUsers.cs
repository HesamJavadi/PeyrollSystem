using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollSystem.Infrastructure.SQL.Migrations
{
    /// <inheritdoc />
    public partial class changeUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "pepCode",
                schema: "ps",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pepCode",
                schema: "ps",
                table: "AspNetUsers");
        }
    }
}

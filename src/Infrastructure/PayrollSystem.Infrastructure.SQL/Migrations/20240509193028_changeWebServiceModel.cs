using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollSystem.Infrastructure.SQL.Migrations
{
    /// <inheritdoc />
    public partial class changeWebServiceModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegDate",
                schema: "ps",
                table: "WebServiceManagement");

            migrationBuilder.DropColumn(
                name: "RegModifyDate",
                schema: "ps",
                table: "WebServiceManagement");

            migrationBuilder.DropColumn(
                name: "RegModifyUser",
                schema: "ps",
                table: "WebServiceManagement");

            migrationBuilder.DropColumn(
                name: "RegUser",
                schema: "ps",
                table: "WebServiceManagement");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegDate",
                schema: "ps",
                table: "WebServiceManagement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RegModifyDate",
                schema: "ps",
                table: "WebServiceManagement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RegModifyUser",
                schema: "ps",
                table: "WebServiceManagement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RegUser",
                schema: "ps",
                table: "WebServiceManagement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

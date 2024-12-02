using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseFinderAPI.Migrations
{
    /// <inheritdoc />
    public partial class TableChangeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Houses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

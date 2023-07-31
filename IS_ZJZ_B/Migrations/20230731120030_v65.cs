using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IS_ZJZ_B.Migrations
{
    /// <inheritdoc />
    public partial class v65 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "place_of_departure",
                table: "gcard",
                newName: "therapy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "therapy",
                table: "gcard",
                newName: "place_of_departure");
        }
    }
}

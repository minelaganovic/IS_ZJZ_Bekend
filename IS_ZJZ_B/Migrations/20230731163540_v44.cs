using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IS_ZJZ_B.Migrations
{
    /// <inheritdoc />
    public partial class v44 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "today_date",
                table: "gcard",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "today_date",
                table: "gcard");
        }
    }
}

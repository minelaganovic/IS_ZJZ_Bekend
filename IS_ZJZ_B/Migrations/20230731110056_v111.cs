using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IS_ZJZ_B.Migrations
{
    /// <inheritdoc />
    public partial class v111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date",
                table: "expensess");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "expensess",
                newName: "path");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "path",
                table: "expensess",
                newName: "status");

            migrationBuilder.AddColumn<string>(
                name: "date",
                table: "expensess",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

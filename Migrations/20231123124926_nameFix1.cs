using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Endeudator.Migrations
{
    /// <inheritdoc />
    public partial class nameFix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Debts",
                newName: "TotalUF");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalUF",
                table: "Debts",
                newName: "Total");
        }
    }
}

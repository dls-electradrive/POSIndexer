using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSIndexer.Migrations
{
    /// <inheritdoc />
    public partial class FixedPartTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Part_Cars_CarId",
                table: "Part");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Part",
                table: "Part");

            migrationBuilder.RenameTable(
                name: "Part",
                newName: "Parts");

            migrationBuilder.RenameIndex(
                name: "IX_Part_CarId",
                table: "Parts",
                newName: "IX_Parts_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parts",
                table: "Parts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Cars_CarId",
                table: "Parts",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Cars_CarId",
                table: "Parts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parts",
                table: "Parts");

            migrationBuilder.RenameTable(
                name: "Parts",
                newName: "Part");

            migrationBuilder.RenameIndex(
                name: "IX_Parts_CarId",
                table: "Part",
                newName: "IX_Part_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Part",
                table: "Part",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Part_Cars_CarId",
                table: "Part",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

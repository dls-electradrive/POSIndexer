using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSIndexer.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Part_Car_Car_Id",
                table: "Part");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Part",
                table: "Part");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.RenameTable(
                name: "Part",
                newName: "Parts");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Cars");

            migrationBuilder.RenameIndex(
                name: "IX_Part_Car_Id",
                table: "Parts",
                newName: "IX_Parts_Car_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parts",
                table: "Parts",
                column: "Part_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Car_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Cars_Car_Id",
                table: "Parts",
                column: "Car_Id",
                principalTable: "Cars",
                principalColumn: "Car_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Cars_Car_Id",
                table: "Parts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parts",
                table: "Parts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Parts",
                newName: "Part");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Car");

            migrationBuilder.RenameIndex(
                name: "IX_Parts_Car_Id",
                table: "Part",
                newName: "IX_Part_Car_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Part",
                table: "Part",
                column: "Part_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "Car_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Part_Car_Car_Id",
                table: "Part",
                column: "Car_Id",
                principalTable: "Car",
                principalColumn: "Car_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

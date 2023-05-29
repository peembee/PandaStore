using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PandaStore.Migrations
{
    /// <inheritdoc />
    public partial class deltedunneccaceryvariablesinreciptncustomerproducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProducts_Receipts_FK_ReceiptID",
                table: "CustomerProducts");

            migrationBuilder.DropIndex(
                name: "IX_CustomerProducts_FK_ReceiptID",
                table: "CustomerProducts");

            migrationBuilder.DropColumn(
                name: "FK_ReceiptID",
                table: "CustomerProducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FK_ReceiptID",
                table: "CustomerProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProducts_FK_ReceiptID",
                table: "CustomerProducts",
                column: "FK_ReceiptID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProducts_Receipts_FK_ReceiptID",
                table: "CustomerProducts",
                column: "FK_ReceiptID",
                principalTable: "Receipts",
                principalColumn: "ReceiptID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

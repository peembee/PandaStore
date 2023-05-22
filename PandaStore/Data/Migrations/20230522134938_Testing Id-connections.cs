using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PandaStore.Migrations
{
    /// <inheritdoc />
    public partial class TestingIdconnections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_AspNetUsers_FK_PandaUserID",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProducts_AspNetUsers_FK_PandaUserID",
                table: "CustomerProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_AspNetUsers_FK_PandaUserID",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "PandaUserID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "FK_PandaUserID",
                table: "OrderDetails",
                newName: "FK_Id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_FK_PandaUserID",
                table: "OrderDetails",
                newName: "IX_OrderDetails_FK_Id");

            migrationBuilder.RenameColumn(
                name: "FK_PandaUserID",
                table: "CustomerProducts",
                newName: "FK_Id");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerProducts_FK_PandaUserID",
                table: "CustomerProducts",
                newName: "IX_CustomerProducts_FK_Id");

            migrationBuilder.RenameColumn(
                name: "FK_PandaUserID",
                table: "Campaigns",
                newName: "FK_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Campaigns_FK_PandaUserID",
                table: "Campaigns",
                newName: "IX_Campaigns_FK_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_AspNetUsers_FK_Id",
                table: "Campaigns",
                column: "FK_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProducts_AspNetUsers_FK_Id",
                table: "CustomerProducts",
                column: "FK_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_AspNetUsers_FK_Id",
                table: "OrderDetails",
                column: "FK_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_AspNetUsers_FK_Id",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProducts_AspNetUsers_FK_Id",
                table: "CustomerProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_AspNetUsers_FK_Id",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "FK_Id",
                table: "OrderDetails",
                newName: "FK_PandaUserID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_FK_Id",
                table: "OrderDetails",
                newName: "IX_OrderDetails_FK_PandaUserID");

            migrationBuilder.RenameColumn(
                name: "FK_Id",
                table: "CustomerProducts",
                newName: "FK_PandaUserID");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerProducts_FK_Id",
                table: "CustomerProducts",
                newName: "IX_CustomerProducts_FK_PandaUserID");

            migrationBuilder.RenameColumn(
                name: "FK_Id",
                table: "Campaigns",
                newName: "FK_PandaUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Campaigns_FK_Id",
                table: "Campaigns",
                newName: "IX_Campaigns_FK_PandaUserID");

            migrationBuilder.AddColumn<string>(
                name: "PandaUserID",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_AspNetUsers_FK_PandaUserID",
                table: "Campaigns",
                column: "FK_PandaUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProducts_AspNetUsers_FK_PandaUserID",
                table: "CustomerProducts",
                column: "FK_PandaUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_AspNetUsers_FK_PandaUserID",
                table: "OrderDetails",
                column: "FK_PandaUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOAProject.Migrations
{
    public partial class AddAssetEmployees3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AssetsEmployees_AssetId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_AssetId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "Assets");

            migrationBuilder.CreateIndex(
                name: "IX_AssetsEmployees_AssetId",
                table: "AssetsEmployees",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetsEmployees_Assets_AssetId",
                table: "AssetsEmployees",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetsEmployees_Assets_AssetId",
                table: "AssetsEmployees");

            migrationBuilder.DropIndex(
                name: "IX_AssetsEmployees_AssetId",
                table: "AssetsEmployees");

            migrationBuilder.AddColumn<int>(
                name: "AssetId",
                table: "Assets",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetId",
                table: "Assets",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AssetsEmployees_AssetId",
                table: "Assets",
                column: "AssetId",
                principalTable: "AssetsEmployees",
                principalColumn: "Id");
        }
    }
}

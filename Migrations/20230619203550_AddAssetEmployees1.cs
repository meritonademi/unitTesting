using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SOAProject.Migrations
{
    public partial class AddAssetEmployees1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AssetsEmployees",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "AssetId",
                table: "Assets",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetsEmployees",
                table: "AssetsEmployees",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AssetsEmployees_AssetId",
                table: "Assets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetsEmployees",
                table: "AssetsEmployees");

            migrationBuilder.DropIndex(
                name: "IX_Assets_AssetId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AssetsEmployees");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "Assets");
        }
    }
}

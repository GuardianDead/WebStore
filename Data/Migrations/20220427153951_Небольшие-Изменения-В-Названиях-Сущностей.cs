using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Migrations
{
    public partial class НебольшиеИзмененияВНазванияхСущностей : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductArticles_ProductModels_ModelId",
                table: "ProductArticles");

            migrationBuilder.AlterColumn<string>(
                name: "ModelId",
                table: "ProductArticles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductArticles_ProductModels_ModelId",
                table: "ProductArticles",
                column: "ModelId",
                principalTable: "ProductModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductArticles_ProductModels_ModelId",
                table: "ProductArticles");

            migrationBuilder.AlterColumn<string>(
                name: "ModelId",
                table: "ProductArticles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductArticles_ProductModels_ModelId",
                table: "ProductArticles",
                column: "ModelId",
                principalTable: "ProductModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

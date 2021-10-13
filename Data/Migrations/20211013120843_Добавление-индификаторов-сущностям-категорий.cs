using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Migrations
{
    public partial class Добавлениеиндификаторовсущностямкатегорий : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_ProductArticles_ProductId",
                table: "CartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritesListProducts_ProductArticles_ProductId",
                table: "FavoritesListProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductModels_Subcategories_SubcategoryName",
                table: "ProductModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_Categories_CategoryName",
                table: "Subcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subcategories",
                table: "Subcategories");

            migrationBuilder.DropIndex(
                name: "IX_Subcategories_CategoryName",
                table: "Subcategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductModels_SubcategoryName",
                table: "ProductModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "SubcategoryName",
                table: "ProductModels");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "FavoritesListProducts",
                newName: "ProductArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoritesListProducts_ProductId",
                table: "FavoritesListProducts",
                newName: "IX_FavoritesListProducts_ProductArticleId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "CartProducts",
                newName: "ProductArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_CartProducts_ProductId",
                table: "CartProducts",
                newName: "IX_CartProducts_ProductArticleId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subcategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Subcategories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Subcategories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubcategoryId",
                table: "ProductModels",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subcategories",
                table: "Subcategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_CategoryId",
                table: "Subcategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductModels_SubcategoryId",
                table: "ProductModels",
                column: "SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_ProductArticles_ProductArticleId",
                table: "CartProducts",
                column: "ProductArticleId",
                principalTable: "ProductArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritesListProducts_ProductArticles_ProductArticleId",
                table: "FavoritesListProducts",
                column: "ProductArticleId",
                principalTable: "ProductArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductModels_Subcategories_SubcategoryId",
                table: "ProductModels",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_Categories_CategoryId",
                table: "Subcategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_ProductArticles_ProductArticleId",
                table: "CartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritesListProducts_ProductArticles_ProductArticleId",
                table: "FavoritesListProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductModels_Subcategories_SubcategoryId",
                table: "ProductModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_Categories_CategoryId",
                table: "Subcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subcategories",
                table: "Subcategories");

            migrationBuilder.DropIndex(
                name: "IX_Subcategories_CategoryId",
                table: "Subcategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductModels_SubcategoryId",
                table: "ProductModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "SubcategoryId",
                table: "ProductModels");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ProductArticleId",
                table: "FavoritesListProducts",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoritesListProducts_ProductArticleId",
                table: "FavoritesListProducts",
                newName: "IX_FavoritesListProducts_ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductArticleId",
                table: "CartProducts",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartProducts_ProductArticleId",
                table: "CartProducts",
                newName: "IX_CartProducts_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subcategories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Subcategories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubcategoryName",
                table: "ProductModels",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subcategories",
                table: "Subcategories",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_CategoryName",
                table: "Subcategories",
                column: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_ProductModels_SubcategoryName",
                table: "ProductModels",
                column: "SubcategoryName");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_ProductArticles_ProductId",
                table: "CartProducts",
                column: "ProductId",
                principalTable: "ProductArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritesListProducts_ProductArticles_ProductId",
                table: "FavoritesListProducts",
                column: "ProductId",
                principalTable: "ProductArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductModels_Subcategories_SubcategoryName",
                table: "ProductModels",
                column: "SubcategoryName",
                principalTable: "Subcategories",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_Categories_CategoryName",
                table: "Subcategories",
                column: "CategoryName",
                principalTable: "Categories",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebStore.Migrations
{
    public partial class БольшиеИзменениеНазванияПолейВСущностяхКакИСамихСущностей : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FavoritesLists_ListFavouritesId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_ProductArticles_ProductArticleId",
                table: "CartProducts");

            migrationBuilder.DropTable(
                name: "FavoritesListProducts");

            migrationBuilder.DropTable(
                name: "ProductsSold");

            migrationBuilder.DropTable(
                name: "FavoritesLists");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "ProductArticles");

            migrationBuilder.RenameColumn(
                name: "ProductArticleId",
                table: "CartProducts",
                newName: "ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_CartProducts_ProductArticleId",
                table: "CartProducts",
                newName: "IX_CartProducts_ArticleId");

            migrationBuilder.CreateTable(
                name: "FavoriteLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoldProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Order = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FavoritesProductsListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteProducts_FavoriteLists_FavoritesProductsListId",
                        column: x => x.FavoritesProductsListId,
                        principalTable: "FavoriteLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavoriteProducts_ProductArticles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "ProductArticles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProducts_ArticleId",
                table: "FavoriteProducts",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProducts_FavoritesProductsListId",
                table: "FavoriteProducts",
                column: "FavoritesProductsListId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FavoriteLists_ListFavouritesId",
                table: "AspNetUsers",
                column: "ListFavouritesId",
                principalTable: "FavoriteLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_ProductArticles_ArticleId",
                table: "CartProducts",
                column: "ArticleId",
                principalTable: "ProductArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FavoriteLists_ListFavouritesId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_ProductArticles_ArticleId",
                table: "CartProducts");

            migrationBuilder.DropTable(
                name: "FavoriteProducts");

            migrationBuilder.DropTable(
                name: "SoldProducts");

            migrationBuilder.DropTable(
                name: "FavoriteLists");

            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "CartProducts",
                newName: "ProductArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_CartProducts_ArticleId",
                table: "CartProducts",
                newName: "IX_CartProducts_ProductArticleId");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "ProductArticles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FavoritesLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritesLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductsSold",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DaysLifeTime = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsSold", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavoritesListProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FavoritesListId = table.Column<int>(type: "int", nullable: true),
                    ProductArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritesListProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoritesListProducts_FavoritesLists_FavoritesListId",
                        column: x => x.FavoritesListId,
                        principalTable: "FavoritesLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavoritesListProducts_ProductArticles_ProductArticleId",
                        column: x => x.ProductArticleId,
                        principalTable: "ProductArticles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoritesListProducts_FavoritesListId",
                table: "FavoritesListProducts",
                column: "FavoritesListId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritesListProducts_ProductArticleId",
                table: "FavoritesListProducts",
                column: "ProductArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FavoritesLists_ListFavouritesId",
                table: "AspNetUsers",
                column: "ListFavouritesId",
                principalTable: "FavoritesLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_ProductArticles_ProductArticleId",
                table: "CartProducts",
                column: "ProductArticleId",
                principalTable: "ProductArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

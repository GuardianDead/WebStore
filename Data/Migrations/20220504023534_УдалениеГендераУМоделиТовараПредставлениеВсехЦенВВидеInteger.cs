using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStore.Migrations
{
    public partial class УдалениеГендераУМоделиТовараПредставлениеВсехЦенВВидеInteger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Carts_CartId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FavoriteLists_ListFavouritesId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_OrderHistories_OrderHistoryId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_ProductArticles_ArticleId",
                table: "CartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProducts_FavoriteLists_FavoritesProductsListId",
                table: "FavoriteProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProducts_ProductArticles_ArticleId",
                table: "FavoriteProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_AddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Deliveries_DeliveryId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductArticles_ProductModels_ModelId",
                table: "ProductArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductModels_Subcategories_SubcategoryId",
                table: "ProductModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductArticles_ArticleId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_Categories_CategoryId",
                table: "Subcategories");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ListFavouritesId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "ProductModels");

            migrationBuilder.DropColumn(
                name: "ListFavouritesId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "FavoritesProductsListId",
                table: "FavoriteProducts",
                newName: "FavoriteListId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteProducts_FavoritesProductsListId",
                table: "FavoriteProducts",
                newName: "IX_FavoriteProducts_FavoriteListId");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Subcategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ArticleId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubcategoryId",
                table: "ProductModels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "ProductModels",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<string>(
                name: "ModelId",
                table: "ProductArticles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TotalCost",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ArticleId",
                table: "FavoriteProducts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Cost",
                table: "Deliveries",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<string>(
                name: "ArticleId",
                table: "CartProducts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderHistoryId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FavoriteListId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FavoriteListId",
                table: "AspNetUsers",
                column: "FavoriteListId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Carts_CartId",
                table: "AspNetUsers",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FavoriteLists_FavoriteListId",
                table: "AspNetUsers",
                column: "FavoriteListId",
                principalTable: "FavoriteLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_OrderHistories_OrderHistoryId",
                table: "AspNetUsers",
                column: "OrderHistoryId",
                principalTable: "OrderHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_ProductArticles_ArticleId",
                table: "CartProducts",
                column: "ArticleId",
                principalTable: "ProductArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProducts_FavoriteLists_FavoriteListId",
                table: "FavoriteProducts",
                column: "FavoriteListId",
                principalTable: "FavoriteLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProducts_ProductArticles_ArticleId",
                table: "FavoriteProducts",
                column: "ArticleId",
                principalTable: "ProductArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_AddressId",
                table: "Orders",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Deliveries_DeliveryId",
                table: "Orders",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductArticles_ProductModels_ModelId",
                table: "ProductArticles",
                column: "ModelId",
                principalTable: "ProductModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductModels_Subcategories_SubcategoryId",
                table: "ProductModels",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductArticles_ArticleId",
                table: "Products",
                column: "ArticleId",
                principalTable: "ProductArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_Categories_CategoryId",
                table: "Subcategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Carts_CartId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FavoriteLists_FavoriteListId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_OrderHistories_OrderHistoryId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_ProductArticles_ArticleId",
                table: "CartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProducts_FavoriteLists_FavoriteListId",
                table: "FavoriteProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProducts_ProductArticles_ArticleId",
                table: "FavoriteProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_AddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Deliveries_DeliveryId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductArticles_ProductModels_ModelId",
                table: "ProductArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductModels_Subcategories_SubcategoryId",
                table: "ProductModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductArticles_ArticleId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_Categories_CategoryId",
                table: "Subcategories");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FavoriteListId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FavoriteListId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "FavoriteListId",
                table: "FavoriteProducts",
                newName: "FavoritesProductsListId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteProducts_FavoriteListId",
                table: "FavoriteProducts",
                newName: "IX_FavoriteProducts_FavoritesProductsListId");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Subcategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ArticleId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "SubcategoryId",
                table: "ProductModels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ProductModels",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "ProductModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ModelId",
                table: "ProductArticles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCost",
                table: "Orders",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ArticleId",
                table: "FavoriteProducts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "Deliveries",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ArticleId",
                table: "CartProducts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "OrderHistoryId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ListFavouritesId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ListFavouritesId",
                table: "AspNetUsers",
                column: "ListFavouritesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Carts_CartId",
                table: "AspNetUsers",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FavoriteLists_ListFavouritesId",
                table: "AspNetUsers",
                column: "ListFavouritesId",
                principalTable: "FavoriteLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_OrderHistories_OrderHistoryId",
                table: "AspNetUsers",
                column: "OrderHistoryId",
                principalTable: "OrderHistories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_ProductArticles_ArticleId",
                table: "CartProducts",
                column: "ArticleId",
                principalTable: "ProductArticles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProducts_FavoriteLists_FavoritesProductsListId",
                table: "FavoriteProducts",
                column: "FavoritesProductsListId",
                principalTable: "FavoriteLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProducts_ProductArticles_ArticleId",
                table: "FavoriteProducts",
                column: "ArticleId",
                principalTable: "ProductArticles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_AddressId",
                table: "Orders",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Deliveries_DeliveryId",
                table: "Orders",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductArticles_ProductModels_ModelId",
                table: "ProductArticles",
                column: "ModelId",
                principalTable: "ProductModels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductModels_Subcategories_SubcategoryId",
                table: "ProductModels",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductArticles_ArticleId",
                table: "Products",
                column: "ArticleId",
                principalTable: "ProductArticles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_Categories_CategoryId",
                table: "Subcategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}

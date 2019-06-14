using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipies.Parse.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Qty = table.Column<double>(nullable: false),
                    Label = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "Nutritions",
                columns: table => new
                {
                    NutritionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Calories = table.Column<string>(nullable: true),
                    Fat = table.Column<string>(nullable: true),
                    TransFat = table.Column<string>(nullable: true),
                    Sodium = table.Column<string>(nullable: true),
                    Carbohydrate = table.Column<string>(nullable: true),
                    Fiber = table.Column<string>(nullable: true),
                    Sugars = table.Column<string>(nullable: true),
                    Protein = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutritions", x => x.NutritionId);
                });

            migrationBuilder.CreateTable(
                name: "Recipies",
                columns: table => new
                {
                    RecipieId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImgName = table.Column<string>(nullable: true),
                    Instructions = table.Column<string>(nullable: true),
                    NutritionId = table.Column<int>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipies", x => x.RecipieId);
                    table.ForeignKey(
                        name: "FK_Recipies_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipies_Nutritions_NutritionId",
                        column: x => x.NutritionId,
                        principalTable: "Nutritions",
                        principalColumn: "NutritionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipieIngredients",
                columns: table => new
                {
                    RecipieIngredientId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Qty = table.Column<double>(nullable: false),
                    RecipieForeignKey = table.Column<int>(nullable: true),
                    IngredientForeignKey = table.Column<int>(nullable: true),
                    IngredientId = table.Column<int>(nullable: false),
                    Label = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipieIngredients", x => x.RecipieIngredientId);
                    table.ForeignKey(
                        name: "FK_RecipieIngredients_Ingredients_IngredientForeignKey",
                        column: x => x.IngredientForeignKey,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipieIngredients_Recipies_RecipieForeignKey",
                        column: x => x.RecipieForeignKey,
                        principalTable: "Recipies",
                        principalColumn: "RecipieId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipieIngredients_IngredientForeignKey",
                table: "RecipieIngredients",
                column: "IngredientForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_RecipieIngredients_RecipieForeignKey",
                table: "RecipieIngredients",
                column: "RecipieForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Recipies_CategoryId",
                table: "Recipies",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipies_NutritionId",
                table: "Recipies",
                column: "NutritionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipieIngredients");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipies");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Nutritions");
        }
    }
}

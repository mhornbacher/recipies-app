using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipies.Parse.Migrations
{
    public partial class Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false)
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
                    Name = table.Column<string>(nullable: false),
                    Unit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "Recipies",
                columns: table => new
                {
                    RecipieId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImgName = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    Instructions = table.Column<string>(type: "text", nullable: true),
                    Quick = table.Column<bool>(nullable: false),
                    Fruit = table.Column<bool>(nullable: false),
                    Reboot = table.Column<bool>(nullable: false),
                    PostWorkout = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipies", x => x.RecipieId);
                    table.ForeignKey(
                        name: "FK_Recipies_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipieIngredients",
                columns: table => new
                {
                    RecipieId = table.Column<int>(nullable: false),
                    IngredientId = table.Column<int>(nullable: false),
                    Qty = table.Column<double>(nullable: false),
                    Optional = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipieIngredients", x => new { x.RecipieId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_RecipieIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipieIngredients_Recipies_RecipieId",
                        column: x => x.RecipieId,
                        principalTable: "Recipies",
                        principalColumn: "RecipieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Name",
                table: "Ingredients",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipieIngredients_IngredientId",
                table: "RecipieIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipies_CategoryId",
                table: "Recipies",
                column: "CategoryId");
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
        }
    }
}

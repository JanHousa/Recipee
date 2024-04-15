using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recipee.Migrations
{
    /// <inheritdoc />
    public partial class updatedmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Instructions = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AverageRating = table.Column<double>(type: "REAL", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "AverageRating", "CreatedDate", "Description", "ImageUrl", "Instructions", "Title" },
                values: new object[,]
                {
                    { 1, 4.5, new DateTime(2024, 4, 15, 21, 16, 44, 518, DateTimeKind.Local).AddTicks(4396), "Bohatý čokoládový dort s třemi vrstvami.", "url_k_obrazku_dortu", "Smíchejte suroviny a pečte na 180°C 50 minut.", "Čokoládový dort" },
                    { 2, 4.0, new DateTime(2024, 4, 15, 21, 16, 44, 518, DateTimeKind.Local).AddTicks(4434), "Klasický Caesar salát s kuřecím masem.", "https://receptypanicuby.cz/wp-content/uploads/2020/08/caesar-salat-recept-5.jpg", "Smíchejte a podávejte čerstvé.", "Caesar salát" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Amount", "Name", "RecipeId" },
                values: new object[] { 1, "200g", "Čokoláda", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankSystem.Migrations
{
    /// <inheritdoc />
    public partial class mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Accounts_AccountEntityId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_AccountEntityId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "AccountEntityId",
                table: "Cards");

            migrationBuilder.CreateTable(
                name: "AccountEntityCardEntity",
                columns: table => new
                {
                    AccountsId = table.Column<int>(type: "int", nullable: false),
                    CardsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountEntityCardEntity", x => new { x.AccountsId, x.CardsId });
                    table.ForeignKey(
                        name: "FK_AccountEntityCardEntity_Accounts_AccountsId",
                        column: x => x.AccountsId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountEntityCardEntity_Cards_CardsId",
                        column: x => x.CardsId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "user", null },
                    { 2, null, "operator", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountEntityCardEntity_CardsId",
                table: "AccountEntityCardEntity",
                column: "CardsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountEntityCardEntity");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "AccountEntityId",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_AccountEntityId",
                table: "Cards",
                column: "AccountEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Accounts_AccountEntityId",
                table: "Cards",
                column: "AccountEntityId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}

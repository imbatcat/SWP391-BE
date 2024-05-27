using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHealthcareSystem.Migrations
{
    /// <inheritdoc />
    public partial class _03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Accounts_AccountId",
                table: "Pets");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Pets",
                type: "char(11)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(11)");

            migrationBuilder.CreateIndex(
                name: "IX_Cages_CageNumber",
                table: "Cages",
                column: "CageNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Accounts_AccountId",
                table: "Pets",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Accounts_AccountId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Cages_CageNumber",
                table: "Cages");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Pets",
                type: "char(11)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(11)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Accounts_AccountId",
                table: "Pets",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

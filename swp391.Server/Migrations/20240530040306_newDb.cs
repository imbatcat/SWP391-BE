using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHealthcareSystem.Migrations
{
    /// <inheritdoc />
    public partial class newDb : Migration
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Accounts_AccountId",
                table: "Pets",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHealthcare.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddCancelandCheckin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCancel",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCheckIn",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancel",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsCheckIn",
                table: "Appointments");
        }
    }
}

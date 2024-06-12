using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHealthcare.Server.Migrations
{
    /// <inheritdoc />
    public partial class db01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<string>(type: "char(11)", nullable: false),
                    AppointmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    AppointmentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppointmentNotes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BookingPrice = table.Column<double>(type: "float", nullable: false),
                    IsCancel = table.Column<bool>(type: "bit", nullable: false),
                    IsCheckIn = table.Column<bool>(type: "bit", nullable: false),
                    IsCheckUp = table.Column<bool>(type: "bit", nullable: false),
                    CheckinTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    AccountId = table.Column<string>(type: "char(11)", nullable: false),
                    PetId = table.Column<string>(type: "char(11)", nullable: false),
                    VeterinarianAccountId = table.Column<string>(type: "char(11)", nullable: false),
                    TimeSlotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Accounts_VeterinarianAccountId",
                        column: x => x.VeterinarianAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_TimeSlots_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "TimeSlotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdmissionRecords",
                columns: table => new
                {
                    AdmissionId = table.Column<string>(type: "char(11)", maxLength: 10, nullable: false),
                    AdmissionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DischargeDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsDischarged = table.Column<bool>(type: "bit", nullable: false),
                    PetCurrentCondition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MedicalRecordId = table.Column<string>(type: "char(11)", nullable: false),
                    VeterinarianAccountId = table.Column<string>(type: "char(11)", nullable: false),
                    PetId = table.Column<string>(type: "char(11)", nullable: false),
                    CageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionRecords", x => x.AdmissionId);
                    table.ForeignKey(
                        name: "FK_AdmissionRecords_Accounts_VeterinarianAccountId",
                        column: x => x.VeterinarianAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdmissionRecords_Cages_CageId",
                        column: x => x.CageId,
                        principalTable: "Cages",
                        principalColumn: "CageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdmissionRecords_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "MedicalRecordId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdmissionRecords_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdmissionRecords");

            migrationBuilder.DropTable(
                name: "BookingPayments");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "ServiceOrderDetails");

            migrationBuilder.DropTable(
                name: "ServicePayments");

            migrationBuilder.DropTable(
                name: "Cages");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "ServiceOrders");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

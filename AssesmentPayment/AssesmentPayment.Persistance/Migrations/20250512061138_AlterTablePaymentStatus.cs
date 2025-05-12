using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssesmentPayment.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AlterTablePaymentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusPayment",
                table: "TrPayment",
                newName: "PaymentStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "TrPayment",
                newName: "StatusPayment");
        }
    }
}

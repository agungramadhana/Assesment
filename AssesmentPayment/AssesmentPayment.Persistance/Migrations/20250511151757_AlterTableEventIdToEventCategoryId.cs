using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssesmentPayment.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableEventIdToEventCategoryId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "TrPayment",
                newName: "EventCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_TrPayment_EventId",
                table: "TrPayment",
                newName: "IX_TrPayment_EventCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventCategoryId",
                table: "TrPayment",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_TrPayment_EventCategoryId",
                table: "TrPayment",
                newName: "IX_TrPayment_EventId");
        }
    }
}

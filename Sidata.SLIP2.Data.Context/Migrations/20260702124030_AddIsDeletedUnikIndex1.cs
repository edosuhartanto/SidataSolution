using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sidata.SLIP2.Data.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedUnikIndex1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InstrumentType_TypeCode",
                table: "InstrumentType");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentType_TypeCode",
                table: "InstrumentType",
                column: "TypeCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InstrumentType_TypeCode",
                table: "InstrumentType");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentType_TypeCode",
                table: "InstrumentType",
                column: "TypeCode");
        }
    }
}

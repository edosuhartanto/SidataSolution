using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sidata.SLIP2.Data.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedUnikIndek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InstrumentType_TypeCode_IsDeleted",
                table: "InstrumentType");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentType_TypeCode",
                table: "InstrumentType",
                column: "TypeCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InstrumentType_TypeCode",
                table: "InstrumentType");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentType_TypeCode_IsDeleted",
                table: "InstrumentType",
                columns: new[] { "TypeCode", "IsDeleted" },
                unique: true);
        }
    }
}

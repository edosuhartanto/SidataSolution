using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sidata.SLIP2.Data.Context.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCreateAtUtcDefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "MerchantSummaryPeriod",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 551, DateTimeKind.Utc).AddTicks(5862));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "MerchantInstrumentType",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 544, DateTimeKind.Utc).AddTicks(7762));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "Merchant",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 548, DateTimeKind.Utc).AddTicks(6389));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "LedgerTransaction",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 554, DateTimeKind.Utc).AddTicks(8493));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "InstrumentType",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 543, DateTimeKind.Utc).AddTicks(7116));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "InstrumentDefinition",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 540, DateTimeKind.Utc).AddTicks(8972));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "InstrumentAccount",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 547, DateTimeKind.Utc).AddTicks(1496));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "IdempotencyRecord",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 553, DateTimeKind.Utc).AddTicks(7877));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "Customer",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 545, DateTimeKind.Utc).AddTicks(9180));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "BalanceBucket",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 552, DateTimeKind.Utc).AddTicks(8117));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "AccountSummaryPeriod",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 550, DateTimeKind.Utc).AddTicks(3968));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "MerchantSummaryPeriod",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 551, DateTimeKind.Utc).AddTicks(5862),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "MerchantInstrumentType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 544, DateTimeKind.Utc).AddTicks(7762),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "Merchant",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 548, DateTimeKind.Utc).AddTicks(6389),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "LedgerTransaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 554, DateTimeKind.Utc).AddTicks(8493),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "InstrumentType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 543, DateTimeKind.Utc).AddTicks(7116),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "InstrumentDefinition",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 540, DateTimeKind.Utc).AddTicks(8972),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "InstrumentAccount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 547, DateTimeKind.Utc).AddTicks(1496),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "IdempotencyRecord",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 553, DateTimeKind.Utc).AddTicks(7877),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "Customer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 545, DateTimeKind.Utc).AddTicks(9180),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "BalanceBucket",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 552, DateTimeKind.Utc).AddTicks(8117),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "AccountSummaryPeriod",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 550, DateTimeKind.Utc).AddTicks(3968),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");
        }
    }
}

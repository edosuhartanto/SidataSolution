using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sidata.SLIP2.Data.Context.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDefaultBehaviorInstrumentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "MerchantSummaryPeriod",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 551, DateTimeKind.Utc).AddTicks(5862),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 37, DateTimeKind.Utc).AddTicks(2812));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "MerchantInstrumentType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 544, DateTimeKind.Utc).AddTicks(7762),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 30, DateTimeKind.Utc).AddTicks(619));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "Merchant",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 548, DateTimeKind.Utc).AddTicks(6389),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 34, DateTimeKind.Utc).AddTicks(1381));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "LedgerTransaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 554, DateTimeKind.Utc).AddTicks(8493),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 40, DateTimeKind.Utc).AddTicks(3081));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "InstrumentType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 543, DateTimeKind.Utc).AddTicks(7116),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 29, DateTimeKind.Utc).AddTicks(532));

            migrationBuilder.AddColumn<bool>(
                name: "DefaultAllowDebit",
                table: "InstrumentType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DefaultAllowNegativeBalance",
                table: "InstrumentType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DefaultAllowTopup",
                table: "InstrumentType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DefaultExpireAfterDays",
                table: "InstrumentType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "DefaultHasExpiration",
                table: "InstrumentType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "DefaultMaximumBalance",
                table: "InstrumentType",
                type: "decimal(20,4)",
                precision: 20,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "DefaultSingleUseOnly",
                table: "InstrumentType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DefaultTransferable",
                table: "InstrumentType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "InstrumentDefinition",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 540, DateTimeKind.Utc).AddTicks(8972),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 26, DateTimeKind.Utc).AddTicks(3429));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "InstrumentAccount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 547, DateTimeKind.Utc).AddTicks(1496),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 32, DateTimeKind.Utc).AddTicks(6789));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "IdempotencyRecord",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 553, DateTimeKind.Utc).AddTicks(7877),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 39, DateTimeKind.Utc).AddTicks(4458));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "Customer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 545, DateTimeKind.Utc).AddTicks(9180),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 31, DateTimeKind.Utc).AddTicks(4263));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "BalanceBucket",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 552, DateTimeKind.Utc).AddTicks(8117),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 38, DateTimeKind.Utc).AddTicks(5390));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "AccountSummaryPeriod",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 550, DateTimeKind.Utc).AddTicks(3968),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 36, DateTimeKind.Utc).AddTicks(1051));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultAllowDebit",
                table: "InstrumentType");

            migrationBuilder.DropColumn(
                name: "DefaultAllowNegativeBalance",
                table: "InstrumentType");

            migrationBuilder.DropColumn(
                name: "DefaultAllowTopup",
                table: "InstrumentType");

            migrationBuilder.DropColumn(
                name: "DefaultExpireAfterDays",
                table: "InstrumentType");

            migrationBuilder.DropColumn(
                name: "DefaultHasExpiration",
                table: "InstrumentType");

            migrationBuilder.DropColumn(
                name: "DefaultMaximumBalance",
                table: "InstrumentType");

            migrationBuilder.DropColumn(
                name: "DefaultSingleUseOnly",
                table: "InstrumentType");

            migrationBuilder.DropColumn(
                name: "DefaultTransferable",
                table: "InstrumentType");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "MerchantSummaryPeriod",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 37, DateTimeKind.Utc).AddTicks(2812),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 551, DateTimeKind.Utc).AddTicks(5862));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "MerchantInstrumentType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 30, DateTimeKind.Utc).AddTicks(619),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 544, DateTimeKind.Utc).AddTicks(7762));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "Merchant",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 34, DateTimeKind.Utc).AddTicks(1381),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 548, DateTimeKind.Utc).AddTicks(6389));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "LedgerTransaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 40, DateTimeKind.Utc).AddTicks(3081),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 554, DateTimeKind.Utc).AddTicks(8493));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "InstrumentType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 29, DateTimeKind.Utc).AddTicks(532),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 543, DateTimeKind.Utc).AddTicks(7116));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "InstrumentDefinition",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 26, DateTimeKind.Utc).AddTicks(3429),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 540, DateTimeKind.Utc).AddTicks(8972));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "InstrumentAccount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 32, DateTimeKind.Utc).AddTicks(6789),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 547, DateTimeKind.Utc).AddTicks(1496));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "IdempotencyRecord",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 39, DateTimeKind.Utc).AddTicks(4458),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 553, DateTimeKind.Utc).AddTicks(7877));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "Customer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 31, DateTimeKind.Utc).AddTicks(4263),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 545, DateTimeKind.Utc).AddTicks(9180));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "BalanceBucket",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 38, DateTimeKind.Utc).AddTicks(5390),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 552, DateTimeKind.Utc).AddTicks(8117));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "AccountSummaryPeriod",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 36, DateTimeKind.Utc).AddTicks(1051),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 6, 26, 14, 30, 0, 550, DateTimeKind.Utc).AddTicks(3968));
        }
    }
}

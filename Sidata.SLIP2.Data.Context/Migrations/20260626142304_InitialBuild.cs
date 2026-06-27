using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sidata.SLIP2.Data.Context.Migrations
{
    /// <inheritdoc />
    public partial class InitialBuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountSummaryPeriod",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentAccountId = table.Column<long>(type: "bigint", nullable: false),
                    AccountingPeriodYear = table.Column<int>(type: "int", nullable: false),
                    AccountingPeriodMonth = table.Column<int>(type: "int", nullable: false),
                    OpeningBalance = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    TotalEarnAmount = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    TotalRedeemAmount = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    TotalExpireAmount = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    TotalOtherAmount = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    ClosingBalance = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ClosedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 36, DateTimeKind.Utc).AddTicks(1051)),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "dev-sys"),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSummaryPeriod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdempotencyRecord",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdempotencyKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdempotencyRecordStatus = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    RequestHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RequestData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpireAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 39, DateTimeKind.Utc).AddTicks(4458)),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "dev-sys"),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdempotencyRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 29, DateTimeKind.Utc).AddTicks(532)),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "dev-sys"),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Merchant",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MerchantName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 34, DateTimeKind.Utc).AddTicks(1381)),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "dev-sys"),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MerchantSummaryPeriod",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantId = table.Column<long>(type: "bigint", nullable: false),
                    AccountingPeriodYear = table.Column<int>(type: "int", nullable: false),
                    AccountingPeriodMonth = table.Column<int>(type: "int", nullable: false),
                    TotalCustomerCount = table.Column<int>(type: "int", precision: 20, scale: 4, nullable: false, defaultValue: 0),
                    TotalActiveCustomerCount = table.Column<int>(type: "int", precision: 20, scale: 4, nullable: false, defaultValue: 0),
                    TotalAccountCount = table.Column<int>(type: "int", precision: 20, scale: 4, nullable: false, defaultValue: 0),
                    TotalActiveAccountCount = table.Column<int>(type: "int", precision: 20, scale: 4, nullable: false, defaultValue: 0),
                    TotalOpeningBalance = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    TotalClosingBalance = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    TotalEarnAmount = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    TotalRedeemAmount = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    TotalExpireAmount = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    TotalOtherAmount = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 37, DateTimeKind.Utc).AddTicks(2812)),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "dev-sys"),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantSummaryPeriod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantId = table.Column<long>(type: "bigint", nullable: false),
                    SimariCustomerId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CustomerNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 31, DateTimeKind.Utc).AddTicks(4263)),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "dev-sys"),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Merchant_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InstrumentDefinition",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantId = table.Column<long>(type: "bigint", nullable: false),
                    InstrumentTypeId = table.Column<long>(type: "bigint", nullable: false),
                    DefinitionCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DefinitionName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BalanceStrategyType = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    EarnConversionRate = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    EarnRoundingMode = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    EarnRoundingFactor = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    RedeemConversionRate = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    RedeemRoundingMode = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    RedeemRoundingFactor = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AllowTopup = table.Column<bool>(type: "bit", nullable: false),
                    AllowDebit = table.Column<bool>(type: "bit", nullable: false),
                    HasExpiration = table.Column<bool>(type: "bit", nullable: false),
                    ExpireAfterDays = table.Column<int>(type: "int", nullable: false),
                    MaximumBalance = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false),
                    SingleUseOnly = table.Column<bool>(type: "bit", nullable: false),
                    Transferable = table.Column<bool>(type: "bit", nullable: false),
                    AllowNegativeBalance = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 26, DateTimeKind.Utc).AddTicks(3429)),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "dev-sys"),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstrumentDefinition_InstrumentType_InstrumentTypeId",
                        column: x => x.InstrumentTypeId,
                        principalTable: "InstrumentType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InstrumentDefinition_Merchant_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MerchantInstrumentType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantId = table.Column<long>(type: "bigint", nullable: false),
                    InstrumentTypeId = table.Column<long>(type: "bigint", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 30, DateTimeKind.Utc).AddTicks(619)),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "dev-sys"),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantInstrumentType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MerchantInstrumentType_InstrumentType_InstrumentTypeId",
                        column: x => x.InstrumentTypeId,
                        principalTable: "InstrumentType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MerchantInstrumentType_Merchant_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InstrumentAccount",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    InstrumentDefinitionId = table.Column<long>(type: "bigint", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    ReservedBalance = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    Status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    ActivatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PinHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PinLockedUntilUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 32, DateTimeKind.Utc).AddTicks(6789)),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "dev-sys"),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstrumentAccount_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InstrumentAccount_InstrumentDefinition_InstrumentDefinitionId",
                        column: x => x.InstrumentDefinitionId,
                        principalTable: "InstrumentDefinition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InstrumentAccount_Merchant_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BalanceBucket",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentAccountId = table.Column<long>(type: "bigint", nullable: false),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false),
                    OriginalAmount = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    ConsumedAmount = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    EarnedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 38, DateTimeKind.Utc).AddTicks(5390)),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "dev-sys"),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceBucket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BalanceBucket_InstrumentAccount_InstrumentAccountId",
                        column: x => x.InstrumentAccountId,
                        principalTable: "InstrumentAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LedgerTransaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantId = table.Column<long>(type: "bigint", nullable: false),
                    InstrumentAccountId = table.Column<long>(type: "bigint", nullable: false),
                    TransactionReferenceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountingPeriodYear = table.Column<int>(type: "int", nullable: false),
                    AccountingPeriodMonth = table.Column<int>(type: "int", nullable: false),
                    TransactionDateAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionType = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    Amount = table.Column<decimal>(type: "decimal(20,4)", precision: 20, scale: 4, nullable: false, defaultValue: 0m),
                    IdempotencyRecordId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    ExternalReferenceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BranchReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MachineReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2026, 6, 26, 14, 23, 4, 40, DateTimeKind.Utc).AddTicks(3081)),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "dev-sys"),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerTransaction_InstrumentAccount_InstrumentAccountId",
                        column: x => x.InstrumentAccountId,
                        principalTable: "InstrumentAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LedgerTransaction_Merchant_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountSummaryPeriod_InstrumentAccountId",
                table: "AccountSummaryPeriod",
                column: "InstrumentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSummaryPeriod_InstrumentAccountId_AccountingPeriodYear_AccountingPeriodMonth",
                table: "AccountSummaryPeriod",
                columns: new[] { "InstrumentAccountId", "AccountingPeriodYear", "AccountingPeriodMonth" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountSummaryPeriod_IsClosed",
                table: "AccountSummaryPeriod",
                column: "IsClosed");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSummaryPeriod_IsDeleted",
                table: "AccountSummaryPeriod",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceBucket_InstrumentAccountId_SequenceNumber",
                table: "BalanceBucket",
                columns: new[] { "InstrumentAccountId", "SequenceNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BalanceBucket_IsDeleted",
                table: "BalanceBucket",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_IsDeleted",
                table: "Customer",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_MerchantId_CustomerNumber",
                table: "Customer",
                columns: new[] { "MerchantId", "CustomerNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_SimariCustomerId",
                table: "Customer",
                column: "SimariCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_IdempotencyRecord_IdempotencyKey_RequestHash",
                table: "IdempotencyRecord",
                columns: new[] { "IdempotencyKey", "RequestHash" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdempotencyRecord_IsDeleted",
                table: "IdempotencyRecord",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentAccount_CustomerId",
                table: "InstrumentAccount",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentAccount_InstrumentDefinitionId",
                table: "InstrumentAccount",
                column: "InstrumentDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentAccount_IsDeleted",
                table: "InstrumentAccount",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentAccount_MerchantId_AccountNumber",
                table: "InstrumentAccount",
                columns: new[] { "MerchantId", "AccountNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentAccount_Status",
                table: "InstrumentAccount",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentDefinition_InstrumentTypeId",
                table: "InstrumentDefinition",
                column: "InstrumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentDefinition_IsDeleted",
                table: "InstrumentDefinition",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentDefinition_MerchantId_DefinitionCode",
                table: "InstrumentDefinition",
                columns: new[] { "MerchantId", "DefinitionCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentType_IsDeleted",
                table: "InstrumentType",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentType_TypeCode",
                table: "InstrumentType",
                column: "TypeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LedgerTransaction_IdempotencyRecordId",
                table: "LedgerTransaction",
                column: "IdempotencyRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerTransaction_InstrumentAccountId",
                table: "LedgerTransaction",
                column: "InstrumentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerTransaction_IsDeleted",
                table: "LedgerTransaction",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerTransaction_MerchantId_AccountingPeriodYear_AccountingPeriodMonth",
                table: "LedgerTransaction",
                columns: new[] { "MerchantId", "AccountingPeriodYear", "AccountingPeriodMonth" });

            migrationBuilder.CreateIndex(
                name: "IX_LedgerTransaction_MerchantId_TransactionReferenceNumber",
                table: "LedgerTransaction",
                columns: new[] { "MerchantId", "TransactionReferenceNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Merchant_IsDeleted",
                table: "Merchant",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Merchant_MerchantCode",
                table: "Merchant",
                column: "MerchantCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MerchantInstrumentType_InstrumentTypeId",
                table: "MerchantInstrumentType",
                column: "InstrumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantInstrumentType_IsDeleted",
                table: "MerchantInstrumentType",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantInstrumentType_MerchantId_InstrumentTypeId",
                table: "MerchantInstrumentType",
                columns: new[] { "MerchantId", "InstrumentTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MerchantSummaryPeriod_IsDeleted",
                table: "MerchantSummaryPeriod",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantSummaryPeriod_MerchantId",
                table: "MerchantSummaryPeriod",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantSummaryPeriod_MerchantId_AccountingPeriodYear_AccountingPeriodMonth",
                table: "MerchantSummaryPeriod",
                columns: new[] { "MerchantId", "AccountingPeriodYear", "AccountingPeriodMonth" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountSummaryPeriod");

            migrationBuilder.DropTable(
                name: "BalanceBucket");

            migrationBuilder.DropTable(
                name: "IdempotencyRecord");

            migrationBuilder.DropTable(
                name: "LedgerTransaction");

            migrationBuilder.DropTable(
                name: "MerchantInstrumentType");

            migrationBuilder.DropTable(
                name: "MerchantSummaryPeriod");

            migrationBuilder.DropTable(
                name: "InstrumentAccount");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "InstrumentDefinition");

            migrationBuilder.DropTable(
                name: "InstrumentType");

            migrationBuilder.DropTable(
                name: "Merchant");
        }
    }
}

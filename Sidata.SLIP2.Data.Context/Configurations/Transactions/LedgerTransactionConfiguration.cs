using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sidata.Abstractions.DataContext.Enums;
using Sidata.Abstractions.Queryable.SqlServer.Extensions;
using Sidata.SLIP2.Data.Enums;
using Sidata.SLIP2.Data.Transactions;

namespace Sidata.SLIP2.Data.Context.Configurations.Transactions
{
    public sealed class LedgerTransactionConfiguration
        : IEntityTypeConfiguration<LedgerTransaction>
    {
        public void Configure(
            EntityTypeBuilder<LedgerTransaction> builder)
        {
            builder.ConfigurePersistentObject("LedgerTransaction");

            // without default value, means caller should provide the value explicitly
            builder.ConfigureCodeStringProperty(x => x.TransactionReferenceNumber, 
                                                RequiredMode.Yes);
            builder.ConfigureIntegerProperty(x => x.AccountingPeriodYear, 
                                             RequiredMode.Yes);
            builder.ConfigureIntegerProperty(x => x.AccountingPeriodMonth, 
                                             RequiredMode.Yes);

            builder.ConfigureUtcDateTimeProperty(x => x.TransactionDateAtUtc,
                                                 RequiredMode.Yes);

            builder.ConfigureEnumProperty(x => x.TransactionType,
                                          RequiredMode.Yes,
                                          LedgerTransactionType.Unknown);

            builder.ConfigureDecimalProperty(x => x.Amount, 20, 4, 
                                             RequiredMode.Yes, 
                                             0);

            // meski idempotencyId belum tentu ada, bisa diisi 0
            // namun dibikin required, agar db tidak terisi null
            builder.ConfigureIntegerProperty(x => x.IdempotencyRecordId, 
                                             RequiredMode.Yes, 0);

            // this all string is allow to be nulled
            builder.ConfigureDescriptionStringProperty(x => x.ExternalReferenceNumber);
            builder.ConfigureCodeStringProperty(x => x.BranchReference);
            builder.ConfigureCodeStringProperty(x => x.MachineReference);
            builder.ConfigureMaxStringProperty(x => x.Remark);

            // FK : semua harus required
            builder.ConfigureForeignKey(t => t.Merchant, t => t.MerchantId,
                                        DeleteBehavior.NoAction, RequiredMode.Yes);
            builder.ConfigureForeignKey(t => t.InstrumentAccount, t => t.InstrumentAccountId,
                                        DeleteBehavior.NoAction, RequiredMode.Yes);

            // indexes
            // connection to Period
            builder.ConfigureIndexes(t => new { t.MerchantId, t.AccountingPeriodYear, t.AccountingPeriodMonth });
            // jika dibutuhkan utk mencari berdasarkan idempotency record
            builder.ConfigureIndexes(t => t.IdempotencyRecordId);
            // unique key
            // reference number ... must be unique per merchant
            builder.ConfigureIndexes(t => new { t.MerchantId, t.TransactionReferenceNumber }, UniqueMode.Yes);
        }
    }
}

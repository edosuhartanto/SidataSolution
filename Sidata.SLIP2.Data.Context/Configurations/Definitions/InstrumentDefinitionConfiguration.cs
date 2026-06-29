
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sidata.Abstractions.DataContext.Enums;
using Sidata.Abstractions.Queryable.SqlServer.Extensions;
using Sidata.SLIP2.Data.Definitions;
using Sidata.SLIP2.Data.Enums;

namespace Sidata.SLIP2.Data.Context.Configurations.Definitions
{
    public sealed class InstrumentDefinitionConfiguration
        : IEntityTypeConfiguration<InstrumentDefinition>
    {
        public void Configure(
            EntityTypeBuilder<InstrumentDefinition> builder)
        {
            builder.ConfigurePersistentObject("InstrumentDefinition");
            // without default value, means caller should provide the value explicitly
            builder.ConfigureString50Property(x => x.DefinitionCode, 
                                                RequiredMode.Yes);
            builder.ConfigureString255Property(x => x.DefinitionName, 
                                                       RequiredMode.Yes);
            builder.ConfigureEnumProperty(x => x.BalanceStrategyType, 
                                          RequiredMode.Yes, 
                                          BalanceStrategyType.Undefined);
            // --- earn
            builder.ConfigureDecimalProperty(x => x.EarnConversionRate, 20, 4, 
                                             RequiredMode.Yes, 0);
            builder.ConfigureEnumProperty(x => x.EarnRoundingMode, 
                                          RequiredMode.Yes, 
                                          RoundingMode.None);
            builder.ConfigureDecimalProperty(x => x.EarnRoundingFactor, 20, 4,
                                             RequiredMode.Yes, 0);
            // --- redeem
            builder.ConfigureDecimalProperty(x => x.RedeemConversionRate, 20, 4,
                                             RequiredMode.Yes, 0);
            builder.ConfigureEnumProperty(x => x.RedeemRoundingMode,
                                          RequiredMode.Yes,
                                          RoundingMode.None);
            builder.ConfigureDecimalProperty(x => x.RedeemRoundingFactor, 20, 4,
                                             RequiredMode.Yes, 0);

            builder.ConfigureBooleanProperty(x => x.IsActive,
                                             RequiredMode.Yes, false);

            //-- selectable behavior
            builder.ConfigureBooleanProperty(x => x.AllowTopup);
            builder.ConfigureBooleanProperty(x => x.AllowDebit);
            builder.ConfigureBooleanProperty(x => x.HasExpiration);
            builder.ConfigureIntegerProperty(x => x.ExpireAfterDays);
            builder.ConfigureDecimalProperty(x => x.MaximumBalance, 20, 4);
            builder.ConfigureBooleanProperty(x => x.SingleUseOnly);
            builder.ConfigureBooleanProperty(x => x.Transferable);
            builder.ConfigureBooleanProperty(x => x.AllowNegativeBalance);

            // FK
            builder.ConfigureForeignKey(x => x.Merchant, x => x.MerchantId,
                                        DeleteBehavior.NoAction, RequiredMode.Yes);

            // unique key
            // definition code is unique for each Merchant
            builder.ConfigureIndexes(x => new {x.MerchantId, x.DefinitionCode } , 
                                     UniqueMode.Yes);
    
            // added aggregation one to many relationship
            // CAUTION: do use lazy load for aggregation ... can slowing query performance
            //          always explicit .include for better query performance
            builder.ConfigureAggregation(
                        d => d.InstrumentAccounts,
                        a => a.InstrumentDefinition,
                        a => a.InstrumentDefinitionId);
        }
    }
}

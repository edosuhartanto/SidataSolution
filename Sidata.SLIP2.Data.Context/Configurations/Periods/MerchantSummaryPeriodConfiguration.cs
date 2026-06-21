
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sidata.Abstractions.DataContext.Enums;
using Sidata.Abstractions.Queryable.SqlServer.Extensions;
using Sidata.SLIP2.Data.Periods;

namespace Sidata.SLIP2.Data.Context.Configurations.Periods
{
    public sealed class MerchantSummaryPeriodConfiguration
        : IEntityTypeConfiguration<MerchantSummaryPeriod>
    {
        public void Configure(
            EntityTypeBuilder<MerchantSummaryPeriod> builder)
        {
            builder.ConfigurePersistentObject("MerchantSummaryPeriod");

            // without default value, means caller should provide the value explicitly
            builder.ConfigureIntegerProperty(x => x.AccountingPeriodYear, 
                                             RequiredMode.Yes);
            builder.ConfigureIntegerProperty(x => x.AccountingPeriodMonth, 
                                             RequiredMode.Yes);
            builder.ConfigureDecimalProperty(x => x.TotalCustomerCount, 20, 4,
                                             RequiredMode.Yes, 0);
            builder.ConfigureDecimalProperty(x => x.TotalActiveCustomerCount, 
                                             20, 4, 
                                             RequiredMode.Yes, 0);
            builder.ConfigureDecimalProperty(x => x.TotalAccountCount, 20, 4, 
                                             RequiredMode.Yes, 0);
            builder.ConfigureDecimalProperty(x => x.TotalActiveAccountCount, 
                                             20, 4, 
                                             RequiredMode.Yes, 0);
            builder.ConfigureDecimalProperty(x => x.TotalOpeningBalance,                
                                             20, 4, 
                                             RequiredMode.Yes, 0);
            builder.ConfigureDecimalProperty(x => x.TotalClosingBalance, 20, 4, 
                                             RequiredMode.Yes, 0);
            builder.ConfigureDecimalProperty(x => x.TotalEarnAmount, 20, 4, 
                                             RequiredMode.Yes, 0);
            builder.ConfigureDecimalProperty(x => x.TotalRedeemAmount, 20, 4, 
                                             RequiredMode.Yes, 0);
            builder.ConfigureDecimalProperty(x => x.TotalExpireAmount, 20, 4, 
                                             RequiredMode.Yes, 0);
            builder.ConfigureDecimalProperty(x => x.TotalOtherAmount, 20, 4, 
                                             RequiredMode.Yes, 0);

            // FK
            builder.ConfigureIndexes(x => x.MerchantId);

            // Unique key
            builder.ConfigureIndexes(x => new {
                                        x.MerchantId,
                                        x.AccountingPeriodYear,
                                        x.AccountingPeriodMonth
                                     },
                                     UniqueMode.Yes);
        }
    }
}

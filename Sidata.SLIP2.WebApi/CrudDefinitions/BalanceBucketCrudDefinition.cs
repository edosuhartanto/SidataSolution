// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Sidata.Abstractions.WebApi.Enums;
using Sidata.Abstractions.WebApi.Services;
using Sidata.SLIP2.Data.DTOs.Masters;
using Sidata.SLIP2.Data.DTOs.Transactions;
using Sidata.SLIP2.Data.Masters;
using Sidata.SLIP2.Data.Transactions;
using System.Linq.Expressions;

namespace Sidata.SLIP2.WebApi.CrudDefinitions
{
    public class BalanceBucketCrudDefinition :
        CrudDefinition<BalanceBucket, BalanceBucketDto>
    {
        public override Func<BalanceBucketDto, Expression<Func<BalanceBucket, bool>>>
            InsertDuplicateChecker =>
                (dto) => c => c.InstrumentAccountId == dto.InstrumentAccountId &&
                              c.SequenceNumber == dto.SequenceNumber;
        public override Func<BalanceBucketDto, Expression<Func<BalanceBucket, bool>>>
            UpdateDuplicateChecker =>
                (dto) => c => c.InstrumentAccountId == dto.InstrumentAccountId &&
                              c.SequenceNumber == dto.SequenceNumber &&
                              c.Id != dto.Id;
        public override Action<BalanceBucketDto, BalanceBucket, CopyIdStatus>
            UpdateEntityFromDto =>
                (dto, BalanceBucket, copyid) =>
                {
                    BalanceBucket.OriginalAmount = dto.OriginalAmount;
                    BalanceBucket.ConsumedAmount = dto.ConsumedAmount;
                    BalanceBucket.EarnedAtUtc = dto.EarnedAtUtc;
                    BalanceBucket.ExpireAtUtc = dto.ExpireAtUtc;
                    if (copyid == CopyIdStatus.CopyIt)
                    {
                        BalanceBucket.InstrumentAccountId = dto.InstrumentAccountId;
                        BalanceBucket.SequenceNumber = dto.SequenceNumber;
                        BalanceBucket.Id = dto.Id;
                    }
                };
        public override Func<BalanceBucketDto, BalanceBucket>
            CopyDtoToEntity =>
                (dto) => new()
                {
                    Id = dto.Id,
                    InstrumentAccountId = dto.InstrumentAccountId,
                    SequenceNumber = dto.SequenceNumber,
                    OriginalAmount = dto.OriginalAmount,
                    ConsumedAmount = dto.ConsumedAmount,
                    EarnedAtUtc = dto.EarnedAtUtc,
                    ExpireAtUtc = dto.ExpireAtUtc,
                };
        public override Expression<Func<BalanceBucket, BalanceBucketDto>>
            LinqExpressionEntityToDto =>
                (cust) => new()
                {
                    Id = cust.Id,
                    InstrumentAccountId = cust.InstrumentAccountId,
                    SequenceNumber = cust.SequenceNumber,
                    OriginalAmount = cust.OriginalAmount,
                    ConsumedAmount = cust.ConsumedAmount,
                    EarnedAtUtc = cust.EarnedAtUtc,
                    ExpireAtUtc = cust.ExpireAtUtc,
                };


    }
}
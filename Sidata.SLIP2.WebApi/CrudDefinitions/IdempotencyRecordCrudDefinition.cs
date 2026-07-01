// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Sidata.Abstractions.WebApi.Enums;
using Sidata.Abstractions.WebApi.Services;
using Sidata.SLIP2.Data.DTOs.Masters;
using Sidata.SLIP2.Data.Masters;
using System.Linq.Expressions;

namespace Sidata.SLIP2.WebApi.CrudDefinitions
{
    public class IdempotencyRecordCrudDefinition :
        CrudDefinition<IdempotencyRecord, IdempotencyRecordDto>
    {
        public override Func<IdempotencyRecordDto, Expression<Func<IdempotencyRecord, bool>>>
            InsertDuplicateChecker =>
                (dto) => c => c.IdempotencyKey == dto.IdempotencyKey;
        public override Func<IdempotencyRecordDto, Expression<Func<IdempotencyRecord, bool>>>
            UpdateDuplicateChecker =>
                (dto) => c => c.IdempotencyKey == dto.IdempotencyKey &&
                               c.Id != dto.Id;
        public override Action<IdempotencyRecordDto, IdempotencyRecord, CopyIdStatus>
            UpdateEntityFromDto =>
                (dto, IdempotencyRecord, copyid) =>
                {
                    IdempotencyRecord.Id = dto.Id;
                    IdempotencyRecord.IdempotencyRecordStatus = dto.IdempotencyRecordStatus;
                    IdempotencyRecord.RequestHash = dto.RequestHash;
                    IdempotencyRecord.RequestData = dto.RequestData;
                    IdempotencyRecord.ExpireAtUtc = dto.ExpireAtUtc;
                    if (copyid == CopyIdStatus.CopyIt)
                    {
                        IdempotencyRecord.IdempotencyKey = dto.IdempotencyKey;
                        IdempotencyRecord.Id = dto.Id;
                    }
                };
        public override Func<IdempotencyRecordDto, IdempotencyRecord>
            CopyDtoToEntity =>
                (dto) => new()
                {
                    Id = dto.Id,
                    IdempotencyKey = dto.IdempotencyKey,
                    IdempotencyRecordStatus = dto.IdempotencyRecordStatus,
                    RequestHash = dto.RequestHash,
                    RequestData = dto.RequestData,
                    ExpireAtUtc = dto.ExpireAtUtc
                };
        public override Expression<Func<IdempotencyRecord, IdempotencyRecordDto>>
            LinqExpressionEntityToDto =>
                (cust) => new()
                {
                    Id = cust.Id,
                    MerchantId = cust.MerchantId,
                    IdempotencyKey = cust.IdempotencyKey,
                    IdempotencyRecordStatus = cust.IdempotencyRecordStatus,
                    RequestHash = cust.RequestHash,
                    RequestData = cust.RequestData,
                    ExpireAtUtc = cust.ExpireAtUtc
                };


    }
}
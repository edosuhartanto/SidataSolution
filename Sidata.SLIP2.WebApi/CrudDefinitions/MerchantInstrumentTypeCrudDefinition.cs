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
    public class MerchantInstrumentTypeCrudDefinition :
        CrudDefinition<MerchantInstrumentType, MerchantInstrumentTypeDto>
    {
        public override Func<MerchantInstrumentTypeDto, Expression<Func<MerchantInstrumentType, bool>>>
            InsertDuplicateChecker =>
                (dto) => c => c.MerchantId == dto.MerchantId;
        public override Func<MerchantInstrumentTypeDto, Expression<Func<MerchantInstrumentType, bool>>>
            UpdateDuplicateChecker =>
                (dto) => c => c.MerchantId == dto.MerchantId &&
                               c.Id != dto.Id;
        public override Action<MerchantInstrumentTypeDto, MerchantInstrumentType, CopyIdStatus>
            UpdateEntityFromDto =>
                (dto, MerchantInstrumentType, copyid) =>
                {
                    MerchantInstrumentType.IsDisabled = dto.IsDisabled;
                    if (copyid == CopyIdStatus.CopyIt)
                    {
                        MerchantInstrumentType.MerchantId = dto.MerchantId;
                        MerchantInstrumentType.InstrumentTypeId = dto.InstrumentTypeId;
                        MerchantInstrumentType.Id = dto.Id;
                    }
                };
        public override Func<MerchantInstrumentTypeDto, MerchantInstrumentType>
            CopyDtoToEntity =>
                (dto) => new()
                {
                    Id = dto.Id,
                    MerchantId = dto.MerchantId,
                    InstrumentTypeId = dto.InstrumentTypeId,
                    IsDisabled = dto.IsDisabled
                };
        public override Expression<Func<MerchantInstrumentType, MerchantInstrumentTypeDto>>
            LinqExpressionEntityToDto =>
                (cust) => new()
                {
                    Id = cust.Id,
                    MerchantId = cust.MerchantId,
                    InstrumentTypeId = cust.InstrumentTypeId,
                    IsDisabled = cust.IsDisabled
                };


    }
}
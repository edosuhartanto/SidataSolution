// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Sidata.Abstractions.WebApi.Enums;
using Sidata.Abstractions.WebApi.Services;
using Sidata.Auth.Data;
using Sidata.Auth.Data.DTOs;
using System.Linq.Expressions;

namespace Sidata.Auth.WebApi.CrudDefinitions
{
    public class ApiClientCrudDefinition :
        CrudDefinition<ApiClient, ApiClientDto>
    {
        public override Func<ApiClientDto, Expression<Func<ApiClient, bool>>>
            InsertDuplicateChecker =>
                (dto) => c => c.ClientCode == dto.ClientCode;
        public override Func<ApiClientDto, Expression<Func<ApiClient, bool>>>
            UpdateDuplicateChecker =>
                (dto) => c => c.ClientCode == dto.ClientCode &&
                              c.Id != dto.Id;
        public override Action<ApiClientDto, ApiClient, CopyIdStatus>
            UpdateEntityFromDto =>
                (dto, client, copyid) =>
                {
                    client.ClientDescription = dto.ClientDescription;
                    client.ClientKeyHash = dto.ClientKey;
                    client.IsHashed = dto.IsHashed;
                    client.IsActive = dto.IsActive;
                    if (copyid == CopyIdStatus.CopyIt)
                    {
                        client.ClientCode = dto.ClientCode;
                        client.Id = dto.Id;
                    }
                };
        public override Func<ApiClientDto, ApiClient>
            CopyDtoToEntity =>
                (dto) => new()
                {
                    Id = dto.Id,
                    ClientCode = dto.ClientCode,
                    ClientDescription = dto.ClientDescription,
                    ClientKeyHash = dto.ClientKey,
                    IsHashed = dto.IsHashed,
                    IsActive = dto.IsActive
                };
        public override Expression<Func<ApiClient, ApiClientDto>>
            LinqExpressionEntityToDto =>
                (client) => new()
                {
                    Id = client.Id,
                    ClientCode = client.ClientCode,
                    ClientDescription = client.ClientDescription,
                    IsHashed = client.IsHashed,
                    IsActive = client.IsActive
                };


    }
}
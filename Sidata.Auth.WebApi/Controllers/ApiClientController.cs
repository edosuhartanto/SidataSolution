
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sidata.Abstractions.Auth.JWT.Services;
using Sidata.Abstractions.Queryable.Exceptions;
using Sidata.Abstractions.Queryable.Models;
using Sidata.Abstractions.WebApi.Attributes;
using Sidata.Abstractions.WebApi.BaseControllers;
using Sidata.Abstractions.WebApi.Enums;
using Sidata.Abstractions.WebApi.Extensions;
using Sidata.Abstractions.WebApi.Interfaces;
using Sidata.Abstractions.WebApi.ResponseRequest.Extensions;
using Sidata.Abstractions.WebApi.ResponseRequest.Models;
using Sidata.Auth.Data;
using Sidata.Auth.Data.Context;
using Sidata.Auth.Data.DTOs;

namespace Sidata.Auth.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ControllerObjectId(101)]
    public class ApiClientController(
                    IDbContextFactory<AuthDbContext> dbfactory,
                    ICrudDefinition<ApiClient, ApiClientDto> cruddefinition,
                    IJwtTokenService jwt)
        : BaseController<AuthDbContext, ApiClient, ApiClientDto>
            (dbfactory, cruddefinition)
    {
        protected readonly IJwtTokenService _jwt = jwt;

        [HttpPost("registerclient")]
        public async Task<ActionResult<ResponseData<ApiClientDto>>>
            RegisterClient(RequestData<ApiClientDto> request)
        {
            return await EntityCreateAsync(request,
                            (dto) =>
                            {
                                if (dto.ClientKey.Length > 0)
                                {
                                    var p = new PasswordHasher<ApiClientDto>();
                                    dto.ClientKey = p.HashPassword(dto, dto.ClientKey);
                                    dto.IsHashed = true;
                                }
                                else
                                {
                                    dto.IsHashed = false;
                                }
                                dto.IsActive = true;
                            });
        }

        [HttpPost("getlistclient")]
        public async Task<ActionResult<ResponseData<ApiClientDto>>>
            GetListClient(RequestData<QueryContent> request)
        {
            return await BuildListAsync(request);
        }

        [HttpPost("unregisterclient")]
        public async Task<ActionResult<ResponseData<ApiClientDto>>>
            UnregisterClient(RequestData<long> request)
        {
            return await EntityDeleteAsync(request);
        }

        [HttpPost("getaccesstoken")]
        public async Task<ActionResult<ResponseData<string>>>
            GetAccessToken(RequestData<CodeAndKeyDto> request)
        {
            using var db = _dbfactory.CreateDbContext();
            try
            {
                // check and get the content
                request.ThrowIfContentNullOrMultipleItem();
                var dto = request.Contents[0];

                // get from database
                var client =
                    await db.ApiClients
                        .FirstOrDefaultAsync(c => c.ClientCode == dto.ClientCode)
                    ?? throw new EntityNotFoundException($"client {dto.ClientCode} tidak ditemukan");
                if (!client.IsActive)
                    throw new EntityNotActiveException($"client {dto.ClientCode} tidak aktif");

                // check password
                var r = PasswordVerificationResult.SuccessRehashNeeded;
                var p = new PasswordHasher<CodeAndKeyDto>();
                if (client.IsHashed)
                {
                    r = p.VerifyHashedPassword(dto, client.ClientKeyHash, dto.ClientKey);
                }
                else
                {
                    if (client.ClientKeyHash != dto.ClientKey) r = PasswordVerificationResult.Failed;
                }
                switch (r)
                {
                    case PasswordVerificationResult.Failed:
                        throw new PasswordIsnotValidException($"Key client {dto.ClientCode} tidak valid");

                    case PasswordVerificationResult.SuccessRehashNeeded:
                        // rehash password
                        client.ClientKeyHash = p.HashPassword(dto, dto.ClientKey);
                        client.IsHashed = true;
                        db.ApiClients.Update(client);
                        await db.SaveChangesAsync();
                        break;

                    case PasswordVerificationResult.Success:
                        break;
                }

                // now build JWT Token
                var token = _jwt.GenerateAccessToken(client);
                return Ok(token.BuildOkResponseData());
            }
            catch (Exception ex)
            {
                return Ok(
                        ex.BuildErrorResponseData(
                            ControllerObjectIdExtension.Builder(ControllerObjectId, BaseStatementId.AccessToken)));
            }
        }
    }
}


// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sidata.Abstractions.WebApi.Attributes;
using Sidata.Abstractions.WebApi.BaseControllers;
using Sidata.Abstractions.WebApi.Interfaces;
using Sidata.Auth.Data;
using Sidata.Auth.Data.Context;
using Sidata.Auth.Data.DTOs;

namespace Sidata.Auth.WebApi
{
    [ApiController]
    [Route("api/[controller]")]
    [ControllerObjectId(2)]
    public class AppUserController(
                    IDbContextFactory<AuthDbContext> dbfactory,
                    ICrudDefinition<AppUser, AppUserDto> cruddefinition) 
        : WebApiCrudControllerBase<AuthDbContext, AppUser, AppUserDto>
            (dbfactory, cruddefinition)
    { }
}

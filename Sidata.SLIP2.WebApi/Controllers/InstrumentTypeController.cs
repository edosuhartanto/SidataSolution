
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
using Sidata.SLIP2.Data.Context;
using Sidata.SLIP2.Data.DTOs.Masters;
using Sidata.SLIP2.Data.Masters;

namespace Sidata.SLIP2.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags($"1.Masters - 2.{nameof(InstrumentTypeController)}")]
    [ControllerObjectId(2)]
    public class InstrumentTypeController(
                    IDbContextFactory<LoyaltyDbContext> dbfactory,
                    ICrudDefinition<InstrumentType, InstrumentTypeDto> cruddefinition) 
        : WebApiCrudControllerBase<LoyaltyDbContext, InstrumentType, InstrumentTypeDto>
            (dbfactory, cruddefinition)
    { }
}


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
using Sidata.SLIP2.Data.Definitions;
using Sidata.SLIP2.Data.DTOs.Definitions;
using Sidata.SLIP2.Data.DTOs.Masters;
using Sidata.SLIP2.Data.Masters;

namespace Sidata.SLIP2.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags($"1.Masters - 2.{nameof(InstrumentDefinitionController)}")]
    [ControllerObjectId(2)]
    public class InstrumentDefinitionController(
                    IDbContextFactory<LoyaltyDbContext> dbfactory,
                    ICrudDefinition<InstrumentDefinition, InstrumentDefinitionDto> cruddefinition) 
        : WebApiCrudControllerBase<LoyaltyDbContext, InstrumentDefinition, InstrumentDefinitionDto>
            (dbfactory, cruddefinition)
    { }
}

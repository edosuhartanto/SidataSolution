using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sidata.Abstractions.Queryable.Models;
using Sidata.Abstractions.WebApi.Attributes;
using Sidata.Abstractions.WebApi.BaseControllers;
using Sidata.Abstractions.WebApi.Enums;
using Sidata.Abstractions.WebApi.Interfaces;
using Sidata.Abstractions.WebApi.ResponseRequest.Models;
using Sidata.SLIP2.Data.Context;
using Sidata.SLIP2.Data.DTOs.Masters;
using Sidata.SLIP2.Data.Masters;
using System.Linq.Expressions;

namespace Sidata.SLIP2.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ControllerObjectId(1)]
    public class MerchantController
        (IDbContextFactory<LoyaltyDbContext> dbfactory,
         ICrudDefinition<Merchant, MerchantDto> cruddefinition) 
        : WebApiCrudControllerBase<LoyaltyDbContext, Merchant, MerchantDto>
        (dbfactory, cruddefinition)
    { }
}

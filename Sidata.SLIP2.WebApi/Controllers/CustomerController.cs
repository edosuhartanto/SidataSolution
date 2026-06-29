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
    [ControllerObjectId(2)]
    public class CustomerController(
                    IDbContextFactory<LoyaltyDbContext> dbfactory,
                    ICrudDefinition<Customer, CustomerDto> cruddefinition) 
        : WebApiCrudControllerBase<LoyaltyDbContext, Customer, CustomerDto>
            (dbfactory, cruddefinition)
    { }
}

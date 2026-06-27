using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sidata.Abstractions.Queryable.Models;
using Sidata.Abstractions.WebApi.Attributes;
using Sidata.Abstractions.WebApi.BaseControllers;
using Sidata.Abstractions.WebApi.ResponseRequest.Models;
using Sidata.SLIP2.Data.Context;
using Sidata.SLIP2.Data.DTOs.CustomerSlice.Extensions;
using Sidata.SLIP2.Data.DTOs.CustomerSlice.Models;
using Sidata.SLIP2.Data.Masters;

namespace Sidata.SLIP2.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ControllerObjectId(1)]
    public class CustomerController(IDbContextFactory<LoyaltyDbContext> dbfactory) 
        : WebApiBaseController<LoyaltyDbContext, Customer, CustomerDto>(dbfactory)
    {
        //==================================================
        // GET LIST
        //==================================================

        [HttpPost("getlist")]
        public async Task<ActionResult<ResponseData<CustomerDto>>> 
                        GetList(RequestData<QueryContent>? request = null)
        {
            return await BuildListAsync(
                                CustomerDataTransfer.LinqExpressionCustomerToDto, 
                                request);
        }

        //==================================================
        // GET BY ID
        //==================================================

        [HttpPost("getbyid")]
        public async Task<ActionResult<ResponseData<CustomerDto>>> GetById(RequestData<long> id)
        {
            return await BuildByIdAsync(
                    CustomerDataTransfer.LinqExpressionCustomerToDto, 
                    id);
        }

        //==================================================
        // CREATE
        //==================================================

        [HttpPost("createnew")]
        public async Task<ActionResult<ResponseData<CustomerDto>>> 
            CreateNew(RequestData<CustomerDto> request)
        {
            return await EntityCreateAsync(
                (req) => x => x.CustomerNumber == req.CustomerNumber,
                CustomerDataTransfer.CopyDtoToCustomer,
                CustomerDataTransfer.CopyCustomerToDto,
                request);
        }

        //==================================================
        // UPDATE
        //==================================================

        [HttpPost("update")]
        public async Task<ActionResult<ResponseData<CustomerDto>>> Update(
            RequestData<CustomerDto> request)
        {
            return await EntityUpdateAsync(
                (dto) => x => x.CustomerNumber == dto.CustomerNumber && 
                               x.Id != dto.Id,
                (dto, c) => dto.UpdateCustomer(c),
                CustomerDataTransfer.CopyCustomerToDto,
                request);
        }

        //==================================================
        // DELETE (SOFT DELETE)
        //==================================================

        [HttpPost("delete")]
        public async Task<ActionResult<ResponseData<CustomerDto>>> Delete(
            RequestData<CustomerDto> request)
        {
            return await EntityDeleteAsync(
                CustomerDataTransfer.CopyCustomerToDto,
                request);
        }

    }
}

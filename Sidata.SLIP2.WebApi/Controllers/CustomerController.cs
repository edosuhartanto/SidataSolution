using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sidata.Abstractions.Queryable.Models;
using Sidata.Abstractions.WebApi.Attributes;
using Sidata.Abstractions.WebApi.BaseControllers;
using Sidata.Abstractions.WebApi.Enums;
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
                    IDbContextFactory<LoyaltyDbContext> dbfactory) 
        : WebApiCrudControllerBase<LoyaltyDbContext, Customer, CustomerDto>
            (dbfactory)
    {
        //==================================================
        // OVERRIDE ABSTRACT FUNCTIONs
        //==================================================
        protected override Func<Customer, CustomerDto> CopyEntityToDto => 
            (cust) => new()
            {
                Id = cust.Id,
                MerchantId = cust.MerchantId,
                SimariCustomerId = cust.SimariCustomerId,
                CustomerNumber = cust.CustomerNumber,
                Name = cust.Name,
                Email = cust.Email,
                PhoneNumber = cust.PhoneNumber,
                BirthDate = cust.BirthDate,
                IsActive = cust.IsActive
            };

        protected override Expression<Func<Customer, CustomerDto>> 
            LinqExpressionEntityToDto =>
                (cust) => new()
                {
                    Id = cust.Id,
                    MerchantId = cust.MerchantId,
                    SimariCustomerId = cust.SimariCustomerId,
                    CustomerNumber = cust.CustomerNumber,
                    Name = cust.Name,
                    Email = cust.Email,
                    PhoneNumber = cust.PhoneNumber,
                    BirthDate = cust.BirthDate,
                    IsActive = cust.IsActive
                };

        protected override Func<CustomerDto, Expression<Func<Customer, bool>>>
            InsertDuplicateChecker =>
                (dto) => c => c.CustomerNumber == dto.CustomerNumber;
 
        protected override Func<CustomerDto, Expression<Func<Customer, bool>>>
            UpdateDuplicateChecker =>
                (dto) => c => c.CustomerNumber == dto.CustomerNumber &&
                               c.Id != dto.Id;
        
        protected override Action<CustomerDto, Customer, CopyIdStatus>
            UpdateEntityFromDto =>
                (dto, customer, copyid) =>
                {
                    customer.MerchantId = dto.MerchantId;
                    customer.SimariCustomerId = dto.SimariCustomerId;
                    customer.Name = dto.Name;
                    customer.Email = dto.Email;
                    customer.PhoneNumber = dto.PhoneNumber;
                    customer.BirthDate = dto.BirthDate;
                    customer.IsActive = dto.IsActive;
                    if (copyid == CopyIdStatus.CopyIt)
                    {
                        customer.CustomerNumber = dto.CustomerNumber;
                        customer.Id = dto.Id;
                    }
                };
        
        protected override Func<CustomerDto, Customer> 
            CopyDtoToEntity =>
                (dto) => new()
                {
                    Id = dto.Id,
                    MerchantId = dto.MerchantId,
                    SimariCustomerId = dto.SimariCustomerId,
                    CustomerNumber = dto.CustomerNumber,
                    Name = dto.Name,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    BirthDate = dto.BirthDate,
                    IsActive = dto.IsActive
                };


    }
}

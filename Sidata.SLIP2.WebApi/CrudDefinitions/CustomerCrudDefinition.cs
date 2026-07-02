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
    public class CustomerCrudDefinition :
        CrudDefinition<Customer, CustomerDto>
    {
        public override Func<CustomerDto, Expression<Func<Customer, bool>>>
            InsertDuplicateChecker =>
                (dto) => c => c.MerchantId == dto.MerchantId &&
                              c.CustomerNumber == dto.CustomerNumber;
        public override Func<CustomerDto, Expression<Func<Customer, bool>>>
            UpdateDuplicateChecker =>
                (dto) => c => c.MerchantId == dto.MerchantId &&
                              c.CustomerNumber == dto.CustomerNumber &&
                              c.Id != dto.Id;
        public override Action<CustomerDto, Customer, CopyIdStatus>
            UpdateEntityFromDto =>
                (dto, customer, copyid) =>
                {
                    customer.SimariCustomerId = dto.SimariCustomerId;
                    customer.Name = dto.Name;
                    customer.Email = dto.Email;
                    customer.PhoneNumber = dto.PhoneNumber;
                    customer.BirthDate = dto.BirthDate;
                    customer.IsActive = dto.IsActive;
                    if (copyid == CopyIdStatus.CopyIt)
                    {
                        customer.MerchantId = dto.MerchantId;
                        customer.CustomerNumber = dto.CustomerNumber;
                        customer.Id = dto.Id;
                    }
                };
        public override Func<CustomerDto, Customer>
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
        public override Expression<Func<Customer, CustomerDto>>
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


    }
}
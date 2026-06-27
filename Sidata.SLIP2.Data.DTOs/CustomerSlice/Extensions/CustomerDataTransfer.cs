using Sidata.SLIP2.Data.DTOs.Enums;
using Sidata.SLIP2.Data.DTOs.CustomerSlice.Models;
using Sidata.SLIP2.Data.Masters;
using System.Linq.Expressions;

namespace Sidata.SLIP2.Data.DTOs.CustomerSlice.Extensions
{
    public static class CustomerDataTransfer
    {

        /// <summary>
        /// Function to build a Customer based from Dto
        /// </summary>
        public static Func<CustomerDto, Customer> CopyDtoToCustomer =>
            x => new()
            {
                Id = x.Id,
                MerchantId = x.MerchantId,
                SimariCustomerId = x.SimariCustomerId,
                CustomerNumber = x.CustomerNumber,
                Name = x.Name,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                BirthDate = x.BirthDate,
                IsActive = x.IsActive
            };

        /// <summary>
        /// Expression to build Dto from Customer.
        /// use this special in LINQ based expression usually on .Select().
        /// </summary>
        /// <remarks>
        /// please donot use CopyCustomerToDto inside this!
        /// That function is not expression
        /// and cannot be used inside LINQ Expression.
        /// </remarks>
        public static Expression<Func<Customer, CustomerDto>> LinqExpressionCustomerToDto =>
            x => new()
            {
                Id = x.Id,
                MerchantId = x.MerchantId,
                SimariCustomerId = x.SimariCustomerId,
                CustomerNumber = x.CustomerNumber,
                Name = x.Name,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                BirthDate = x.BirthDate,
                IsActive = x.IsActive
            };

        /// <summary>
        /// Copy Customer To Dto. 
        /// Should not be consumed inside LINQ Expression
        /// </summary>
        public static Func<Customer, CustomerDto> CopyCustomerToDto =>
            x => new()
            {
                Id = x.Id,
                MerchantId = x.MerchantId,
                SimariCustomerId = x.SimariCustomerId,
                CustomerNumber = x.CustomerNumber,
                Name = x.Name,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                BirthDate = x.BirthDate,
                IsActive = x.IsActive
            };

        /// <summary>
        /// function to update dto value into current Customer.
        /// if id and code is updated, set copyid = true, default is false
        /// </summary>
        public static void UpdateCustomer(
            this CustomerDto dto, Customer Customer,
            CopyIdStatus copyid = CopyIdStatus.DonotCopy)
        {
            Customer.MerchantId = dto.MerchantId;
            Customer.SimariCustomerId = dto.SimariCustomerId;
            Customer.Name = dto.Name;
            Customer.Email = dto.Email;
            Customer.PhoneNumber = dto.PhoneNumber;
            Customer.BirthDate = dto.BirthDate;
            Customer.IsActive = dto.IsActive;
            if (copyid == CopyIdStatus.CopyIt)
            {
                Customer.CustomerNumber = dto.CustomerNumber;
                Customer.Id = dto.Id;
            }
        }
    }
}

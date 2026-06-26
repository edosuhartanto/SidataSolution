using Sidata.Abstractions.WebApi.Enums;
using Sidata.SLIP2.Data.DTOs.MerchantSlice.Models;
using Sidata.SLIP2.Data.Masters;
using System.Linq.Expressions;

namespace Sidata.SLIP2.Data.DTOs.MerchantSlice.Extensions
{
    public static class MerchantDataTransfer
    {

        /// <summary>
        /// Function to build a merchant based from Dto
        /// </summary>
        public static Func<MerchantDto, Merchant> CopyDtoToMerchant =>
            x => new()
            {
                Id = x.Id,
                MerchantCode = x.MerchantCode,
                MerchantName = x.MerchantName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                IsActive = x.IsActive
            };

        /// <summary>
        /// Expression to build Dto from Merchant.
        /// use this in LINQ based expression usually on .Select().
        /// </summary>
        /// <remarks>
        /// please donot use CopyMerchantToDto inside this!
        /// That function is not expression
        /// and cannot be used inside LINQ Expression.
        /// </remarks>
        public static Expression<Func<Merchant, MerchantDto>> MerchantToDto =>
            x => new()
            {
                Id = x.Id,
                MerchantCode = x.MerchantCode,
                MerchantName = x.MerchantName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                IsActive = x.IsActive
            };

        /// <summary>
        /// Copy Merchant To Dto. 
        /// Should not be consumed inside LINQ Expression
        /// </summary>
        public static Func<Merchant, MerchantDto> CopyMerchantToDto =>
            x => new()
            {
                Id = x.Id,
                MerchantCode = x.MerchantCode,
                MerchantName = x.MerchantName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                IsActive = x.IsActive
            };

        /// <summary>
        /// function to update dto value into current merchant.
        /// if id and code is updated, set copyid = true, default is false
        /// </summary>
        public static void UpdateMerchant(
            this MerchantDto dto, Merchant merchant,
            CopyIdStatus copyid = CopyIdStatus.DonotCopy)
        {
            merchant.MerchantName=dto.MerchantName;
            merchant.Email=dto.Email;
            merchant.PhoneNumber=dto.PhoneNumber;
            merchant.IsActive = dto.IsActive;
            if (copyid == CopyIdStatus.CopyIt)
            {
                merchant.MerchantCode = dto.MerchantCode;
                merchant.Id = dto.Id;
            }
        }
    }
}

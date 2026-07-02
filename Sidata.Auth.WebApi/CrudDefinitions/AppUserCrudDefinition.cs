// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Sidata.Abstractions.WebApi.Enums;
using Sidata.Abstractions.WebApi.Services;
using Sidata.Auth.Data;
using Sidata.Auth.Data.DTOs;
using System.Linq.Expressions;

namespace Sidata.Auth.WebApi.CrudDefinitions
{
    public class AppUserCrudDefinition :
        CrudDefinition<AppUser, AppUserDto>
    {
        public override Func<AppUserDto, Expression<Func<AppUser, bool>>>
            InsertDuplicateChecker =>
                (dto) => c => c.Username == dto.Username ||
                              c.Email == dto.Email;
        public override Func<AppUserDto, Expression<Func<AppUser, bool>>>
            UpdateDuplicateChecker =>
                (dto) => c => (c.Username == dto.Username ||
                               c.Email == dto.Email) 
                           && (c.Id != dto.Id);
        public override Action<AppUserDto, AppUser, CopyIdStatus>
            UpdateEntityFromDto =>
                (dto, user, copyid) =>
                {
                    user.FullName = dto.FullName;
                    user.PhoneNumber = dto.PhoneNumber;
                    user.PasswordHash = dto.PasswordHash;
                    user.IsHashed = dto.IsHashed;
                    user.IsActive = dto.IsActive;
                    if (copyid == CopyIdStatus.CopyIt)
                    {
                        user.Email = dto.Email;
                        user.Username = dto.Username;
                        user.Id = dto.Id;
                    }
                };
        public override Func<AppUserDto, AppUser>
            CopyDtoToEntity =>
                (dto) => new()
                {
                    Id = dto.Id,
                    Username = dto.Username,
                    FullName = dto.FullName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    PasswordHash = dto.PasswordHash,
                    IsHashed = dto.IsHashed,
                    IsActive = dto.IsActive
                };
        public override Expression<Func<AppUser, AppUserDto>>
            LinqExpressionEntityToDto =>
                (user) => new()
                {
                    Id = user.Id,
                    Username = user.Username,
                    FullName = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    PasswordHash = user.PasswordHash,
                    IsHashed = user.IsHashed,
                    IsActive = user.IsActive
                };


    }
}
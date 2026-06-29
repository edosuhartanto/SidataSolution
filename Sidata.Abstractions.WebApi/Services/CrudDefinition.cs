// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Sidata.Abstractions.BaseClasses;
using Sidata.Abstractions.Interfaces;
using Sidata.Abstractions.WebApi.Enums;
using Sidata.Abstractions.WebApi.Interfaces;
using System.Linq.Expressions;

namespace Sidata.Abstractions.WebApi.Services
{
    public abstract class CrudDefinition<TEntity, TDto> 
        : ICrudDefinition<TEntity, TDto>
    where TEntity : PersistentObject
    where TDto : class, IMasterClass
    {
        private Func<TEntity, TDto>? _compiledentitytodto;

        public abstract Func<TDto, Expression<Func<TEntity, bool>>> 
            InsertDuplicateChecker { get; }
        public abstract Func<TDto, Expression<Func<TEntity, bool>>> 
            UpdateDuplicateChecker { get; }
        public abstract Action<TDto, TEntity, CopyIdStatus> 
            UpdateEntityFromDto { get; }
        public abstract Func<TDto, TEntity> 
            CopyDtoToEntity { get; }
        public abstract Expression<Func<TEntity, TDto>> 
            LinqExpressionEntityToDto { get; }
        public Func<TEntity, TDto> CopyEntityToDto =>
            _compiledentitytodto ??= LinqExpressionEntityToDto.Compile();
    }
}
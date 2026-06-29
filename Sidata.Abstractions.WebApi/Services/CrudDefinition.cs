// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Sidata.Abstractions.Interfaces;
using Sidata.Abstractions.WebApi.Enums;
using Sidata.Abstractions.WebApi.Interfaces;
using System.Linq.Expressions;

namespace Sidata.Abstractions.WebApi.Services
{
    /// <summary>
    /// definisi utama utk CRUD. Definisi akan digunakan WebApiCrudControllerBase
    /// untuk menjalankan fungsi utama WebApi CRUD.<br/>
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>Definisi property CopyEntityToDto, tidak boleh diturunkan,
    /// krn sudah diisi dari LinqExpressionCopyEntityToDto yang dikompile,
    /// saat pertama kali diambil.</item>
    /// <item>CrudDefinition harus diturunkan krn ini class abstract</item>
    /// </list>
    /// </remarks>
    public abstract class CrudDefinition<TEntity, TDto> 
        : ICrudDefinition<TEntity, TDto>
    where TEntity : class
    where TDto : class, IMasterClass
    {
        private readonly Func<TEntity, TDto> _compiledentitytodto;

        protected CrudDefinition()
        {
            _compiledentitytodto = LinqExpressionEntityToDto.Compile();
        }

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
        public Func<TEntity, TDto> 
            CopyEntityToDto => _compiledentitytodto;
    }
}
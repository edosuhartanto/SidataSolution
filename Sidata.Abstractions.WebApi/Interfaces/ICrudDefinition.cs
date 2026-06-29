// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Sidata.Abstractions.BaseClasses;
using Sidata.Abstractions.Interfaces;
using Sidata.Abstractions.WebApi.Enums;
using System.Linq.Expressions;

namespace Sidata.Abstractions.WebApi.Interfaces
{
    public interface ICrudDefinition<TEntity, TDto>
    where TEntity : PersistentObject 
    where TDto : class, IMasterClass
    {
        /// <summary>
        /// duplicate checker special for Insert.<br/>
        /// usually to check on Unique Key.
        /// </summary>
        /// <remarks>
        /// the usual = (dto) => (entity) => entity.Kode == dto.Kode
        /// </remarks>
        Func<TDto, Expression<Func<TEntity, bool>>> InsertDuplicateChecker { get; }

        /// <summary>
        /// duplicate checker special for Update.<br/>
        /// the same as insert, but added Id should not the same 
        /// </summary>
        /// <remarks>
        /// the usual = (dto) => (entity) => entity.Kode == dto.Kode && entity.Id != dto.Id
        /// </remarks>
        Func<TDto, Expression<Func<TEntity, bool>>> UpdateDuplicateChecker { get; }

        /// <summary>
        /// controller turunan harus membuat proses update Entity
        /// dgn data yang didapat dari Dto.
        /// </summary>
        /// <remarks>
        /// jika Id dan Kode (yg unik per record) tidak ingin dioverwrite
        /// set CopyIdStatus ke DonotCopy.
        /// </remarks>
        Action<TDto, TEntity, CopyIdStatus> UpdateEntityFromDto { get; }

        /// <summary>
        /// controller turunan harus menurunkan cara mengkopi Dto ke Entity
        /// </summary>
        Func<TDto, TEntity> CopyDtoToEntity { get; }

        /// <summary>
        /// controller turunan harus menurunkan cara mengkopi Dto ke Entity
        /// </summary>
        /// <remarks>
        /// fungsi ini khusus utk digunakan dalam Linq Expression
        /// jadi meskipun mungkin isinya sama seperti yang digunakan dalam 
        /// CopyEntityToDto, 
        /// jangan referensikan ke sini, namun harus dibuatlah sekali lagi.
        /// agar Linq dapat menjadikannya perintah SQL. Kalo tidak, maka
        /// proses perubahan ini akan terjadi di memory, bukan di server SQL.
        /// </remarks>
        Expression<Func<TEntity, TDto>> LinqExpressionEntityToDto { get; }

        /// <summary>
        /// fungsi yang harus diturunkan utk mengkopi entity to dto
        /// </summary>
        Func<TEntity, TDto> CopyEntityToDto { get; }
    }
}
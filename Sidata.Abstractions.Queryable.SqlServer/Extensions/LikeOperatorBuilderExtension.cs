// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sidata.Abstractions.Queryable.Extensions;
using Sidata.Abstractions.Queryable.Models;
using System.Linq.Expressions;

namespace Sidata.Abstractions.Queryable.SqlServer.Extensions
{
    public static class LikeOperatorBuilderExtension
    {
        /// <summary>
        /// register Filter Handler khusus utk operator LIKE dan 
        /// dikhususkan utk provider Sql Server. 
        /// Berupa extension yang bisa dipanggil saat program STARTUP.
        /// </summary>
        public static IServiceCollection AddQueryableLikeOperatorForSqlServer(
            this IServiceCollection services)
        {
            QueryExtensions.RegisterNewFilterHandler(BuildLike); 
            return services;
        }

        public static Expression? BuildLike(
            Expression property,
            FilterContent filtercontent)
        {
            // only receive LIKE operator
            if (filtercontent.Operator == Enums.FilterOperator.Like)
            {
                return Expression.Call(
                    typeof(DbFunctionsExtensions)
                        .GetMethod(
                            nameof(DbFunctionsExtensions.Like),
                            [
                                typeof(DbFunctions),
                                typeof(string),
                                typeof(string)
                            ])!,
                    Expression.Constant(EF.Functions),
                    property,
                    Expression.Constant(filtercontent.Value));
            }
            return null;
        }
    }
}

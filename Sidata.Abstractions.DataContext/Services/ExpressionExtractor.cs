// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using System.Linq.Expressions;

namespace Sidata.Abstractions.DataContext.Services
{
    /// <summary>
    /// kumpulan fungsi untuk mengekstrak isi sebuah LINQ Expression&lt;T&gt;
    /// Tidak type-safe, maka hati2 dalam menggunakannya.
    /// Cocok untuk membuat Filter atau Sort expression
    /// </summary>
    public static class ExpressionExtractor
    {
        /// <summary>
        /// fungsi untuk mendapatkan nama member 
        /// yang dituliskan dalam LINQ Expression&lt;T&gt;
        /// </summary>
        public static string GetMemberName(Expression expression)
        {
            if (expression is MemberExpression member)
                return member.Member.Name;

            if (expression is UnaryExpression unary)
                return GetMemberName(unary.Operand);

            return expression.ToString();
        }

        /// <summary>
        /// fungsi untuk membongkar isi dalam sebuah LINQ Expression&lt;T&gt;,
        /// membongkarnya menjadi Nama Property dan Valuenya ... 
        /// yang sudah dikonversi sesuai tipenya.
        /// </summary>
        public static (string PropertyName, object? Value)
            ExtractMemberInfo<TEntity>(
                Expression<Func<TEntity, bool>> expression)
        {
            if (expression.Body is not BinaryExpression binary)
                return ("Unknown", null);

            string propertyName = GetMemberName(binary.Left);

            object? value =
                Expression
                    .Lambda(binary.Right)
                    .Compile()
                    .DynamicInvoke();

            return (propertyName, value);
        }
    }
}

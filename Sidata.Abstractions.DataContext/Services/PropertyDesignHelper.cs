using System.Linq.Expressions;
using System.Reflection;

namespace Sidata.Abstractions.DataContext.Services
{
    /// <summary>
    /// kumpulan fungsi untuk mendapatkan property info dari sebuah Expression,
    /// ditujukan untuk membangun schema, ORM mapping
    /// </summary>
    public static class PropertyDesignHelper
    {
        /// <summary>
        /// cara mendapatkan info sebuah property dari sebuah Expression,
        /// Dan Expression yang dikirim harus sesuai dengan type yang ditetapkan.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// error, jika parameter selector bukan sebuah property
        /// </exception>
        public static PropertyInfo GetPropertyInfo<TEntity, TProperty>(
            Expression<Func<TEntity, TProperty>> selector)
        {
            MemberExpression member = selector.Body switch
            {
                MemberExpression m => m,
                UnaryExpression u when u.Operand is MemberExpression m => m,
                _ => throw new ArgumentException(
                        "Selector must point to a property.",
                        nameof(selector))
            };

            return member.Member as PropertyInfo
                ?? throw new ArgumentException(
                        "Selector must point to a property.",
                        nameof(selector));
        }

        /// <summary>
        /// utk menilai apakah sebuah Property 
        /// diset NULLABLE (not required) di database server.
        /// </summary>
        public static bool IsNullableProperty<TEntity, TProperty>(
            Expression<Func<TEntity, TProperty>> selector)
        {
            var propertyInfo = GetPropertyInfo(selector);

            var context = new NullabilityInfoContext();

            var nullability = context.Create(propertyInfo);

            return nullability.WriteState ==
                   NullabilityState.Nullable;
        }

        /// <summary>
        /// Hanya butuh nama property dengan type-safe jelas, 
        /// expression dari sebuah property dalam satu entitas.
        /// </summary>
        public static string GetPropertyName<TEntity, TProperty>(
            Expression<Func<TEntity, TProperty>> selector)
        {
            return GetPropertyInfo(selector).Name;
        }
    }
}

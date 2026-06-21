
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sidata.Abstractions.DataContext.Enums;
using Sidata.Abstractions.DataContext.Services;
using System.Linq.Expressions;
using System.Numerics;

namespace Sidata.Abstractions.Queryable.SqlServer.Extensions
{
    
    /// <summary>
    /// extensions for entitytypebuilder
    /// this is special for SqlServer
    /// </summary>
    public static class EntityTypeBuilderExtensions
    {
        #region Index, Keys and Relationship Configurations
        /// <summary>
        /// configure aggregation relationship
        /// build a mapping from an ICollection property on TEntity class as parent
        /// into TChild class as child.
        /// </summary>
        /// <typeparam name="TEntity">class induk</typeparam>
        /// <typeparam name="TChild">class child</typeparam>
        /// <param name="collection">action to select property as collection of child</param>
        /// <param name="parent">action to select property as parent</param>
        /// <param name="fk">action to select property in TEntity as connection id</param>
        /// <param name="deletebehavior">delete behavior, recommendation is noaction</param>
        public static ReferenceCollectionBuilder<TEntity, TChild>
            ConfigureAggregation<TEntity, TChild>(
                this EntityTypeBuilder<TEntity> builder,
                Expression<Func<TEntity, IEnumerable<TChild>?>> collection,
                Expression<Func<TChild, TEntity?>> parent,
                Expression<Func<TChild, object?>> fk,
                DeleteBehavior deletebehavior = DeleteBehavior.NoAction)
                where TEntity : class
                where TChild : class
        {
            return builder
                .HasMany(collection)
                .WithOne(parent)
                .HasForeignKey(fk)
                .OnDelete(deletebehavior);
        }

        /// <summary>
        /// configure foreignkey
        /// </summary>
        public static ReferenceCollectionBuilder<TRelated, TEntity>
            ConfigureForeignKey<TEntity, TRelated>(
                this EntityTypeBuilder<TEntity> builder,
                Expression<Func<TEntity, TRelated?>> navigation,
                Expression<Func<TEntity, object?>> foreignKey,
                DeleteBehavior deletebehavior = DeleteBehavior.NoAction,
                RequiredMode requiredmode = RequiredMode.No)
                where TEntity : class
                where TRelated : class
        {
            var collection = builder
                .HasOne(navigation)
                .WithMany()
                .HasForeignKey(foreignKey)
                .OnDelete(deletebehavior);
            if (requiredmode == RequiredMode.Yes)
            {
                collection.IsRequired(); 
            }
            return collection;
        }

        /// <summary>
        /// configure indexes
        /// </summary>
        public static IndexBuilder ConfigureIndexes<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, object?>> selector,
            UniqueMode uniquemode = UniqueMode.No)
            where TEntity : class
        {
            var property = builder.HasIndex(selector);
            if (uniquemode == UniqueMode.Yes)
            { 
                property.IsUnique();
            }
            return property;
        }
        #endregion

        #region Non-Nullable
        #region Long Type Configuration
        /// <summary>
        /// Configure Long type.
        /// long will never nullable, but if set not required, you need to set the default value.
        /// it will make sure, from anywhere other then this context, the value is always correctly set 0/other value
        /// an explicit declaration is forced to make sure every field in table is correctly build when migration is run
        /// </summary>
        public static PropertyBuilder<long> ConfigureLongIdProperty<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, long>> selector,
            RequiredMode requiredmode,
            long? defaultValue = null)
            where TEntity : class
        {
            //var memberName = ((MemberExpression)selector.Body).Member.Name;
            var memberName = PropertyDesignHelper.GetPropertyInfo(selector).Name;
            var property = builder.Property(selector);
            if (requiredmode == RequiredMode.Yes)
            {
                property.IsRequired();
            }
            if (defaultValue != null)
            {
                property.HasDefaultValue(defaultValue.Value);
            }
            else if (requiredmode == RequiredMode.No)
            {
                // 1.not required and default is null ... is not acceptable ...
                // 2.required but no matter default is ... it can be accepted [for Id, for example]
                // [restiction no.2: is special case ... programmer should know for sure about this]
                throw new ArgumentException($"{memberName} is not required, please configure it to have defaultValue", nameof(defaultValue));
            }
            return property;
        }
        #endregion

        #region Boolean Type Configuration
        /// <summary>
        /// Configure Boolean type.
        /// bool will never nullable, but if set not required, you need to set the default value.
        /// it will make sure, from anywhere other then this context, the value is always correctly set false/true
        /// an explicit declaration is forced to make sure every field in table is correctly build when migration is run
        /// </summary>
        public static PropertyBuilder<bool> ConfigureBooleanProperty<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, bool>> selector,
            RequiredMode requiredmode = RequiredMode.Nullable,
            bool? defaultValue = null)
            where TEntity : class
        {
            //var memberName = ((MemberExpression)selector.Body).Member.Name; 
            var memberName = PropertyDesignHelper.GetPropertyInfo(selector).Name;
            var property = builder.Property(selector);
            if (requiredmode == RequiredMode.Yes)
            {
                property.IsRequired();
            }
            if (defaultValue != null)
            {
                property.HasDefaultValue(defaultValue.Value); 
            }
            else if (requiredmode == RequiredMode.No)
            {
                throw new ArgumentException($"{memberName} is not required, please configure it to have defaultValue", nameof(defaultValue));
            }
            return property;
        }
        #endregion

        #region Enum Type Configuration
        public static PropertyBuilder<TEnum> ConfigureEnumProperty<TEntity, TEnum>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, TEnum>> selector,
            RequiredMode requiredmode = RequiredMode.Nullable,
            TEnum? defaultValue = null)
            where TEntity : class
            where TEnum : struct, Enum
        {
            //var memberName = ((MemberExpression)selector.Body).Member.Name;
            var memberName = PropertyDesignHelper.GetPropertyInfo(selector).Name;
            var property = builder
                .Property(selector);
            if (requiredmode == RequiredMode.Yes)
            {
                property.IsRequired();
            }
            if (defaultValue != null)
            {
                property.HasDefaultValue(defaultValue.Value);
            }
            else if (requiredmode == RequiredMode.No)
            {
                throw new ArgumentException($"{memberName} is not required, please configure it to have defaultValue", nameof(defaultValue));
            }
            return property;
        }

        public static PropertyBuilder<TEnum> ConfigureEnumWithConversionProperty<TEntity, TEnum, TConversion>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, TEnum>> selector,
            RequiredMode requiredmode = RequiredMode.Nullable,
            TEnum? defaultValue = null)
            where TEntity : class
            where TEnum : struct, Enum
        {
            //var memberName = ((MemberExpression)selector.Body).Member.Name;
            var memberName = PropertyDesignHelper.GetPropertyInfo(selector).Name;
            var property = builder
                .Property(selector)
                .HasConversion<TConversion>();
            if (requiredmode == RequiredMode.Yes)
            {
                property.IsRequired();
            }
            if (defaultValue != null)
            {
                property.HasDefaultValue(defaultValue.Value);
            }
            else if (requiredmode == RequiredMode.No)
            {
                throw new ArgumentException($"{memberName} is not required, please configure it to have defaultValue", nameof(defaultValue));
            }
            return property;
        }
        #endregion

        #region Integer Type Configuration
        /// <summary>
        /// Configure Decimal type. Default precision, scale is 20, 4.
        /// decimal will never nullable, so if you set not required, you need to set the default value.
        /// it will make sure, from anywhere other then this context, the value is always correctly set false/true
        /// an explicit declaration is forced to make sure every field in table is correctly build when migration is run
        /// </summary>
        public static PropertyBuilder<TInteger> ConfigureIntegerProperty<TEntity, TInteger>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, TInteger>> selector,
            RequiredMode requiredmode = RequiredMode.Nullable,
            TInteger? defaultValue = null)
            where TEntity : class
            where TInteger : struct, INumber<TInteger>
        {
            //var memberName = ((MemberExpression)selector.Body).Member.Name;
            var memberName = PropertyDesignHelper.GetPropertyInfo(selector).Name;
            var property = builder
                .Property(selector);
            if (requiredmode == RequiredMode.Yes)
            {
                property.IsRequired();
            }
            if (defaultValue != null)
            {
                property.HasDefaultValue(defaultValue.Value);
            }
            else if (requiredmode == RequiredMode.No)
            {
                throw new ArgumentException($"{memberName} is not required, please configure it to have defaultValue", nameof(defaultValue));
            }
            return property;
        }
        #endregion

        #region Decimal Type Configuration
        /// <summary>
        /// Configure Decimal type. Default precision, scale is 20, 4.
        /// decimal will never nullable, so if you set not required, you need to set the default value.
        /// it will make sure, from anywhere other then this context, the value is always correctly set false/true
        /// an explicit declaration is forced to make sure every field in table is correctly build when migration is run
        /// </summary>
        public static PropertyBuilder<decimal> ConfigureDecimalProperty<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, decimal>> selector,
            int precision = 20,
            int scale = 4,
            RequiredMode requiredmode = RequiredMode.Nullable,
            decimal? defaultValue = null)
            where TEntity : class
        {
            //var memberName = ((MemberExpression)selector.Body).Member.Name;
            var memberName = PropertyDesignHelper.GetPropertyInfo(selector).Name;
            var property = builder
                .Property(selector)
                .HasPrecision(precision, scale);
            if (requiredmode == RequiredMode.Yes)
            {
                property.IsRequired();
            }
            if (defaultValue != null)
            {
                property.HasDefaultValue(defaultValue.Value);
            }
            else if (requiredmode == RequiredMode.No)
            {
                throw new ArgumentException($"{memberName} is not required, please configure it to have defaultValue", nameof(defaultValue));
            }
            return property;
        }
        #endregion
        #endregion

        #region Nullable Type
        #region Datetime Type Configuration
        public static PropertyBuilder<DateTime?> ConfigureUtcDateTimeProperty<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, DateTime?>> selector,
            RequiredMode requiredmode = RequiredMode.Nullable,
            DateTime? defaultValue = null)
            where TEntity : class
        {
            //var memberName = ((MemberExpression)selector.Body).Member.Name;
            var memberName = PropertyDesignHelper.GetPropertyInfo(selector).Name;
            var isnullable = PropertyDesignHelper.IsNullableProperty(selector);
            var property = builder.Property(selector);
            // if nullable property, then ignore other parameter 
            if (!isnullable)
            {
                // required and defaultvalue only for non nullable
                // date is special property ... it should have default value, required or not
                // required default      result
                // no       null         err default should not null 
                // no       not null     ok  
                // yes      null         err default shoule not null 
                // yes      not null     ok

                if (requiredmode == RequiredMode.Yes)
                {
                    property.IsRequired();
                }
                if (defaultValue != null)
                {
                    property.HasDefaultValue(defaultValue.Value);
                }
                else if (requiredmode == RequiredMode.No)
                {
                    if (!isnullable)
                    {
                        // date is special property 
                        // if defaultvalue null, and not required, it should have default
                        // if required 
                        // ex. createDate is required, but should not have default, so it should explicitly set
                        // transactionDate isnot required, should have  as default
                        throw new ArgumentException($"{memberName} is not required, please configure it to have defaultValue", nameof(defaultValue));
                    }
                }
            }
            else
            {
                if (requiredmode == RequiredMode.Yes)
                {
                    throw new ArgumentException($"{memberName} is nullable, no need to configure RequiredMode = Yes", nameof(requiredmode));
                }
                if (defaultValue != null)
                {
                    throw new ArgumentException($"{memberName} is nullable, no need to configure defaultValue", nameof(defaultValue));
                }
            }
            return property;
        }
        #endregion

        #region String Type Configuration
        /// <summary>
        /// string property usually used by undefined length (jsondata, document, dll)
        /// length = max. NOTE: use with care for this nvarchar(max) mode. 
        /// cannot be indexed, etc 
        /// </summary>
        public static PropertyBuilder<string?> ConfigureMaxStringProperty<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, string?>> selector,
            RequiredMode requiredMode = RequiredMode.Nullable,
            string? defaultValue = null)
            where TEntity : class
        {
            return builder.ConfigureStringProperty(selector, 0,
                                                   requiredMode, 
                                                   defaultValue);
        }

        /// <summary>
        /// string property usually used by description, remarks, etc length = 255
        /// </summary>
        public static PropertyBuilder<string?> ConfigureDescriptionStringProperty<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, string?>> selector,
            RequiredMode requiredMode = RequiredMode.Nullable,
            string? defaultValue = null)
            where TEntity : class
        {
            return builder.ConfigureStringProperty(selector, 255,
                                                   requiredMode, 
                                                   defaultValue);
        }

        /// <summary>
        /// string property usually used by code, nobukti, etc, with length=50
        /// </summary>
        public static PropertyBuilder<string?> ConfigureCodeStringProperty<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, string?>> selector,
            RequiredMode requiredMode = RequiredMode.Nullable,
            string? defaultValue = null)
            where TEntity : class
        {
            return builder.ConfigureStringProperty(selector, 50,
                                                   requiredMode, 
                                                   defaultValue); 
        }

        public static PropertyBuilder<string?> ConfigureStringProperty<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, string?>> selector,
            int maxLength = 0,
            RequiredMode requiredMode = RequiredMode.Nullable,
            string? defaultValue = null)
            where TEntity : class
        {
            //var memberName = ((MemberExpression)selector.Body).Member.Name;
            var memberName = PropertyDesignHelper.GetPropertyInfo(selector).Name;
            var isnullable = PropertyDesignHelper.IsNullableProperty(selector);
            var property = builder.Property(selector);
            // maxlength harus antara 1 - 4000,
            // jika tidak diantara itu, akan dianggap tanpa maxlength (varchar(max))
            if (maxLength > 0)
            {
                if (maxLength <= 4000)
                {
                    property.HasMaxLength(maxLength);
                }
            }
            if (!isnullable)
            {
                // required kah?
                if (requiredMode == RequiredMode.Yes)
                {
                    property.IsRequired();
                }
                // ada default value?
                if (defaultValue != null)
                {
                    string def = defaultValue;
                    property.HasDefaultValue(def);
                }
                else if (requiredMode == RequiredMode.No)
                {
                    if (!isnullable)
                    {
                        // remember, required property, should not matter have default or not
                        // code is required, but should not have default
                        // address isnot required, should have string.Empty as default
                        throw new ArgumentException($"{memberName} is not required, please configure it to have defaultValue", nameof(defaultValue));
                    }
                }
            } 
            else
            {
                if (requiredMode == RequiredMode.Yes)
                {
                    throw new ArgumentException($"{memberName} is nullable, no need to configure RequiredMode = Yes", nameof(requiredMode));
                }
                if (defaultValue != null)
                {
                    throw new ArgumentException($"{memberName} is nullable, no need to configure defaultValue", nameof(defaultValue));
                }
            }
            return property;
        }
        #endregion
        #endregion
    }
}

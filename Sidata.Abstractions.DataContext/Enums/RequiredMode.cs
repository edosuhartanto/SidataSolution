// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

namespace Sidata.Abstractions.DataContext.Enums
{
    /// <summary>
    /// required mode of a persistent property in dbContext
    /// No = property is not a nullable in C#, but it is set as ALLOW NULL in db, 
    ///      this type of property must have default value
    /// Yes = property is set as NOT NULL, the default value can be set or not
    /// Nullable = property is a nullable in C#, and in db also set as ALLOW NULL, 
    ///            and donot need any default value
    /// type is byte (tinyint)
    /// </summary>
    public enum RequiredMode : byte
    {
        No = 0,
        Yes = 1,
        Nullable = 2
    }
}

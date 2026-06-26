// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
namespace Sidata.Abstractions.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class ControllerObjectIdAttribute(int objectId) 
                : Attribute
    {
        public int Id { get; } = objectId;
    }
}
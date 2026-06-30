
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using System.Reflection;

namespace Sidata.Abstractions.Services
{
	public static class AssemblyProperties
	{
		public static string GetProductDescription()
		{
			return Assembly.GetEntryAssembly()?
							.GetCustomAttribute<AssemblyDescriptionAttribute>()?
							.Description
						?? GetProductName();
		}

		public static string GetProductName()
		{
			return Assembly.GetEntryAssembly()?
							.GetCustomAttribute<AssemblyProductAttribute>()?
							.Product
						?? nameof(AssemblyProperties);
		}

		public static string GetProductVersion()
		{
			return Assembly.GetEntryAssembly()?
							.GetName()?
							.Version?
							.ToString() 
						?? "0.0.1";
		}
	}
}

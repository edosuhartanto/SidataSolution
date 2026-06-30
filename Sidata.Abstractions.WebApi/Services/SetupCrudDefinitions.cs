
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// revision by ChatGpt
// ******************************************************

using Microsoft.Extensions.DependencyInjection;
using Sidata.Abstractions.WebApi.Interfaces;
using System.Reflection;

namespace Sidata.Abstractions.WebApi.Services
{
    public static class SetupCrudDefinitions
    {
        /// <summary>
        /// Fungsi extension utk register class xxxCrudDefinition 
        /// bila class2 tersebut berada di satu assembly yang sama 
        /// dengan assembly pemanggil fungsi ini
        /// </summary>
        /// <remarks>
        /// jika berada di assembly yang berbeda2,
        /// gunakan .AddCrudDefinitions([assembly-1, assembly-2, ..., assembly-n]).<br/>
        /// lihat <seealso cref="AddCrudDefinitions(IServiceCollection, Assembly[])"/> 
        /// untuk lebih jelasnya
        /// </remarks>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCrudDefinitions(this IServiceCollection services)
        {
            return services.AddCrudDefinitions([]);
        }

        /// <summary>
        /// fungsi utk register class xxxCrudDefinition dan 
        /// berada di assembly yang berbeda2.
        /// </summary>
        /// <remarks>
        /// gunakan [typeof(MerchantCrudDefinition).Assembly, 
        /// typeof(CustomerCrudDefinition).Assembly], jika berada
        /// kedua class itu berada di assembly yang berbeda
        /// </remarks>
        /// <param name="assemblies">
        /// array of assembly names, didapat dari "typeof(T).Assembly"
        /// </param>
        public static IServiceCollection AddCrudDefinitions(
            this IServiceCollection services,
            params Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
                assemblies = [Assembly.GetCallingAssembly()];

            foreach (var assembly in assemblies)
            {
                RegisterCrudDefinitions(services, assembly);
            }

            return services;
        }

        private static void RegisterCrudDefinitions(
            IServiceCollection services,
            Assembly assembly)
        {
            var registrations = assembly.GetTypes()
                .Where(t =>
                    t.IsClass &&
                    !t.IsAbstract)
                .SelectMany(t => t.GetInterfaces()
                    .Where(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(ICrudDefinition<,>))
                    .Select(i => new
                    {
                        Service = i,
                        Implementation = t
                    }));

            foreach (var r in registrations)
            {
                services.AddSingleton(r.Service, r.Implementation);
            }
        }
        
    }
}

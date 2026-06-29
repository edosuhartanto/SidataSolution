using Sidata.Abstractions.WebApi.Interfaces;
using Sidata.SLIP2.Data.DTOs.Masters;
using Sidata.SLIP2.Data.Masters;
using Sidata.SLIP2.WebApi.CrudDefinitions;

namespace Sidata.SLIP2.WebApi.Services
{
    public static class SetupCrudDefinitions
    {
        public static IServiceCollection AddCrudDefinition(this IServiceCollection services)
        {
            return 
                services
                .AddSingleton<ICrudDefinition<Customer, CustomerDto>>(new CustomerCrudDefinition())
                .AddSingleton<ICrudDefinition<Merchant, MerchantDto>>(new MerchantCrudDefinition());
        }

    }
}

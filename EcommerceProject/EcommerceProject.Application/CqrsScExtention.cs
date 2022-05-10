using EcommerceProject.Infrastructure.CQRS;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EcommerceProject.Application
{
    public static class CqrsScExtention
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCqrs(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}

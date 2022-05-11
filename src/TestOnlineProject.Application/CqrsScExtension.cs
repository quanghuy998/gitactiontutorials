using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestOnlineProject.Infrastructure.CQRS;

namespace TestOnlineProject.Application
{
    public static class CqrsScExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCqrs(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}

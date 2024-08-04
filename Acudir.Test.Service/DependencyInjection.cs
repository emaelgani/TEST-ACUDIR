using Microsoft.Extensions.DependencyInjection;
using Acudir.Test.Data;
using Acudir.Test.Service.Interfaces;
using Acudir.Test.Service.Services;
using Microsoft.Extensions.Configuration;

namespace Acudir.Test.Service
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddAcudirServiceLayerDependencyInjections(this IServiceCollection service, IConfiguration configuration)
        {

            

            service.AddAcudirDataLayerDependencyInjections(configuration);

            service.AddScoped<IPersonaService, PersonaService>();


            return service;
        }
    }
}

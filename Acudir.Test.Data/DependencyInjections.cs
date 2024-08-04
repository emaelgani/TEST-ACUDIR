using Acudir.Test.Data.Interfaces;
using Acudir.Test.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Acudir.Test.Data
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddAcudirDataLayerDependencyInjections(this IServiceCollection service, IConfiguration configuration)
        {
            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\Acudir.Test.Data"));
            var filePath = Path.Combine(basePath, "Test.json");
            service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddScoped<IPersonaRepository>(provider => new PersonaRepository(filePath));

            return service;
        }
    }
}

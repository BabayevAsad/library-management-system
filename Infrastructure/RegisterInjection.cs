using System.Reflection;
using Api.Base;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ApplicationInjection
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            var aa = Assembly.GetExecutingAssembly().GetExportedTypes()
                .Where(type => type.IsClass
                               && !type.IsAbstract
                               && type != typeof(BaseRepository<>)
                               && type.GetInterfaces().Any(i =>
                                   i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBaseRepository<>)))
                .ToList();
            
                aa.ForEach(type =>
                {
                    var nestedInterface = type.GetInterfaces().First(i =>
                        !i.IsGenericType && i.GetInterfaces().Any(e =>
                            e.IsGenericType && e.GetGenericTypeDefinition() == typeof(IBaseRepository<>)));
                    services.AddScoped(nestedInterface, type);
                });
        }
    }
}
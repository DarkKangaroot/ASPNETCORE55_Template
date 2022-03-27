using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.ConfigureServices
{
    public static class Dependency
    {
        /// <summary>
        ///  Extention that use dependency injection for Repository and Service,do not put other injection 
        ///  that not related to db model or Infra and Service.
        /// </summary>
        /// <param name="services">Statup serivce</param>
        public static void Dependenies(this IServiceCollection services)
        {
            services.AddTransient(typeof(_IGenericsRepository<>), typeof(_GenericsRepository<>));

        }

        /// <summary>
        ///  Extention that use dependency injection for fluent validator. do not put other injection that not related to validator.
        /// </summary>
        /// <param name="services">Statup serivce</param>
        public static void FluentValidators(this IServiceCollection services)
        {

        }
    }
}

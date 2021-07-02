using Domain.Handlers;
using Domain.Infra.Repositories;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
namespace Api.Helpers
{
    public static class DomainHelper
    {

        public static void ConfigureDb(this IServiceCollection services)
        {
            services.AddSingleton<Domain.Infra.Contexts.ProductCatalogContext>();
        }

        public static void ConfigureHandler(this IServiceCollection services) 
        {

            services.AddTransient<LoginHandler, LoginHandler>();
            services.AddTransient<ProductHandler, ProductHandler>();
        }
            


        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}

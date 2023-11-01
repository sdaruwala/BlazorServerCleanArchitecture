using Application.Interfaces;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Common;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMappings();
            services.AddDbContext(configuration);
            services.AddRepositories();
            services.AddFeatureFlags(configuration);
        }

        //private static void AddMappings(this IServiceCollection services)
        //{
        //    services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //}



        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString,
                   builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddTransient<IStadiumRepository, StadiumRepository>();
        }

        private static void AddFeatureFlags(this IServiceCollection services, IConfiguration configuration)
        {
            var featureFlags = configuration.GetSection("FeatureFlags");

            services.AddTransient<IFeatureFlags, FeatureManager>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var featureManager = new FeatureManager(featureFlags);
                return featureManager;
            });

        }
    }
}

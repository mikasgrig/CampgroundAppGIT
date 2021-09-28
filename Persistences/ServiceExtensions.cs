using System;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Persistences.Repositories;

namespace Persistences
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            SqlMapper.AddTypeHandler(new MySqlGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));

            return services
                .AddSqlClient(configuration)
                .AddRepositories();
        }
        
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddSingleton<ICampgroundRepository, CampgroundRepository>()
                .AddSingleton<ICommentRepository, CommentRepository>()
                .AddSingleton<IImageRepository, ImageRepository>()
                .AddSingleton<IUserRepository, UserRepository>();

        }

        private static IServiceCollection AddSqlClient(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration
                .GetSection("ConnectionStrings")
                .GetSection("SqlConnectionString").Value;

            return services.AddTransient<ISqlClient>(_ => new SqlClient(connectionString));
        }
    }
}
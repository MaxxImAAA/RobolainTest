using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Robolain.DAL.Repositories;
using Robolain.Domain.Interfaces.Repositories;
using Robolain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robolain.DAL.DependencyInjections
{
    public static class DependencyInjection
    {
        
        public static void AddDAL(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresSQL");

            services.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseNpgsql(connectionString);
            });

            AddRepositories(services);
            AddServices(services);

        }

        private static void AddServices(IServiceCollection services)
        {

        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Product>, BaseRepository<Product>>();
            services.AddScoped<IBaseRepository<ProductCategory>, BaseRepository<ProductCategory>>();
        }
    }
}

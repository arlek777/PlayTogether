﻿using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlayTogether.BusinessLogic;
using PlayTogether.DataAccess;
using PlayTogether.WebClient.Models;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace PlayTogether.WebClient.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigurePlayTogetherServices(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment env)
        {
            ConfigureDb(services, configuration, env);
            ConfigureSecurity(services, configuration);
            ConfigureBusinessLogic(services);
        }

        private static void ConfigureDb(IServiceCollection services, IConfiguration configuration, IHostingEnvironment env)
        {
            var connStringName = env.IsDevelopment() ? "LocalConnection" : "RemoteConnection";
            var connString = configuration.GetConnectionString(connStringName);

            services.AddTransient<IGenericRepository, EntityFrameworkRepository>();
            services.AddScoped<DbContext>((provider) => new PlayTogetherDbContext(connString));
        }

        private static void ConfigureBusinessLogic(IServiceCollection services)
        {
            services.AddTransient<ISimpleCRUDService, SimpleCRUDService>();
        }

        private static void ConfigureSecurity(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddTransient<JWTTokenProvider>();
        }
    }
}

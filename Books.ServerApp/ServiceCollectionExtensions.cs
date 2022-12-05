using App.Common.Repositories;
using App.Common.Repositories.Abstractions;
using Books.ServerApp.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Books.ServerApp.Services.BooksService.Abstractions;
using Books.ServerApp.Services.BooksService;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Data.SqlClient;

namespace Books.ServerApp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            var connectionString = GetConnectionString();

            services.AddDbContext<BooksDbContext>(options
                => options
                .UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork<BooksDbContext>>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddScoped<IBooksService, BooksService>();

            return service;
        }


        private static string GetConnectionString()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var s = ConfigurationManager.ConnectionStrings["BooksDbPath"].ConnectionString;
            var connectionStringBuilder = 
                new SqlConnectionStringBuilder(s);
            connectionStringBuilder.AttachDBFilename = path + connectionStringBuilder.AttachDBFilename;

            return connectionStringBuilder.ConnectionString;
        }
    }
}
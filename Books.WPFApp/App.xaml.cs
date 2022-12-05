using Books.ServerApp;
using Books.ServerApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using AutoMapper;

namespace Books.WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static void RunServices()
            {
                var services = new ServiceCollection();
                ConfigureServices(services);
                MigrateDbContext<BooksDbContext>(services);
            }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddDatabase();
            services.AddServices();
            services.AddScoped<IMapper, Mapper>();
        }

        private static void MigrateDbContext<TContext>(IServiceCollection services)
            where TContext : DbContext
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var context = serviceProvider.GetRequiredService<TContext>();
                context.Database.Migrate();
            }
        }
    }
}
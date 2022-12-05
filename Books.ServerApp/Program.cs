using Books.ServerApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Books.ServerApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            //AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);

            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //var services = new ServiceCollection();
            //ConfigureServices(services);
            //MigrateDbContext<BooksDbContext>(services);

            //Application.Run(new Form1());
        }


        //private static void ConfigureServices(ServiceCollection services)
        //{
        //    services.AddDatabase();
        //    services.AddServices();
        //}

        //private static void MigrateDbContext<TContext>(IServiceCollection services)
        //    where TContext : DbContext
        //{
        //    using (var serviceProvider = services.BuildServiceProvider())
        //    {
        //        var context = serviceProvider.GetRequiredService<TContext>();
        //        context.Database.Migrate();
        //    }
        //}
    }
}
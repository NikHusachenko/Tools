using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using Tools.Desktop.Pages;
using Tools.Desktop.Windows;
using Tools.EntityFramework;
using Tools.EntityFramework.GenericRepository;
using Tools.Services.DocumentServices;
using Tools.Services.ToolGroupServices;
using Tools.Services.ToolServices;
using Tools.Services.ToolSubgroupServices;

namespace Tools.Desktop
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Configurations
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Services
            services.AddTransient<IToolGroupService, ToolGroupService>();
            services.AddTransient<IToolSubgroupService, ToolSubgroupService>();
            services.AddTransient<IToolService, ToolService>();
            services.AddTransient<IDocumentService, DocumentService>();

            // Validators

            // Pages
            services.AddTransient<CatalogWindow>();
            services.AddTransient<EditEquipmentPage>();
            services.AddTransient<CertificationLayoutPage>();
            services.AddTransient<EquipmentPage>();
            services.AddTransient<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Requared to save default data
            ApplicationDbContext dbContext = _serviceProvider.GetService<ApplicationDbContext>();
            ApplicationContextInitializer.InitializeDefaultData(dbContext);

            MainWindow main = _serviceProvider.GetRequiredService<MainWindow>();
            main.Show();
        }
    }
}
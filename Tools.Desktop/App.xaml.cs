using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using Tools.Desktop.Pages;

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

            // Services

            // Validators

            // Pages
            services.AddTransient<EquipmentPage>();
            services.AddTransient<EditEquipmentPage>();
            services.AddTransient<CertificationLayoutPage>();

            services.AddTransient<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow main = _serviceProvider.GetRequiredService<MainWindow>();
            main.Show();
        }
    }
}
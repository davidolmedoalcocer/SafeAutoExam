using Microsoft.Extensions.DependencyInjection;
using SafeAuto.Core.Interfaces;
using SafeAuto.Core.Services;
using SafeAuto.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SafeAutoWin
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureService(services);

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var frmMain = serviceProvider.GetRequiredService<FrmMain>();

                //Application.Run(new Form1());

                Application.Run(frmMain);
            }
        }

        private static void ConfigureService(ServiceCollection services)
        {
            services.AddScoped<IDriverService, DriverService>()
                .AddScoped<FrmMain>();

            services.AddScoped<IDriverRepository, DriverRepository>()
                .AddScoped<FrmMain>();
        }
    }
}

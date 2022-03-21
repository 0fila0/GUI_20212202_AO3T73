using System;
using System.Windows;
using HarciKalapacs.Logic;
using HarciKalapacs.Model;
using HarciKalapacs.Repository;
using Microsoft.Extensions.DependencyInjection;
using SoundsRenderer;

namespace HarciKalapacs.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Services
            services.AddSingleton<IRepository, Repository.Repository>();
            services.AddSingleton<IModel, Model.Model>();
            services.AddSingleton<IMusic, SoundsRenderer.Music>();
            services.AddSingleton<IGeneralLogic, Logic.GeneralLogic>();
            services.AddSingleton<IInGameLogic, Logic.InGameLogic>();

            return services.BuildServiceProvider();
        }
    }
}

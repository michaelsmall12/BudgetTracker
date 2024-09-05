using BudgetTracker.Services.Interfaces;
using BudgetTracker.Services.Services;
using BudgetTracker.ViewModels;
using BudgetTracker.Views;
using MaterialDesignThemes.Wpf;
using Serilog;
using Prism.Ioc;
using Prism.Regions;
using System.Windows;
using Serilog.Core;

namespace BudgetTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        // Declare Serilog ILogger as a static property
        private static ILogger _logger;

        // Property to access the logger
        public static ILogger Logger => _logger;
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            // Ensure the regions are available after initialization
            var regionManager = Container.Resolve<IRegionManager>();

            // Navigate to Login View after the app is fully initialized
            regionManager.RequestNavigate("ContentRegion", "LoginView");
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day) // You can add additional sinks here (e.g., File, Seq, etc.)
                .CreateLogger();
            Log.Logger = _logger;
            base.OnStartup(e);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<ILogger>(_logger);
            containerRegistry.RegisterSingleton<IUserRepository, UserRepository>();
            containerRegistry.RegisterSingleton<ISnackbarMessageQueue, SnackbarMessageQueue>();
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
        }
    }
}

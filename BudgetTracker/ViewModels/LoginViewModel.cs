using BudgetTracker.Entites;
using BudgetTracker.Services.Interfaces;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Serilog;
using System.Threading.Tasks;

namespace BudgetTracker.ViewModels
{
    public class LoginViewModel : BindableBase, INavigationAware
    {
        private DelegateCommand _loginCommand;
        /// <summary>
        /// Gets or sets the login command
        /// </summary>
        public DelegateCommand LoginCommand
        {
            get => _loginCommand;
            set => SetProperty(ref _loginCommand, value);
        }

        private DelegateCommand _setupCommand;
        /// <summary>
        /// Gets or sets the login command
        /// </summary>
        public DelegateCommand SetupCommand
        {
            get => _setupCommand;
            set => SetProperty(ref _setupCommand, value);
        }

        private string password;
        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private string userName;
        /// <summary>
        /// Gets or sets the UserName
        /// </summary>
        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        /// <summary>
        /// Gets or sets the region Manager
        /// </summary>
        public  IRegionManager RegionManager {get;set;}

        /// <summary>
        /// Gets or sets the User Repositoty
        /// </summary>
        public IUserRepository UserRepository { get;set;}

        public ISnackbarMessageQueue SnackbarMessageQueue { get;set;}

        public ILogger Logger { get;set;}
        /// <summary>
        /// Constructor for the LoginViewModel
        /// </summary>
        /// <param name="userRepository">The user repository to be used in the LoginViewModel</param>
        /// <param name="regionManager">The region manager to be used in the LoginViewModel</param>
        /// <param name="snackbarMessageQueue">"The Snackbar Message Queue to be used in the LoginViewMode</param>
        /// <param name="logger">"The logger to be used in the LoginViewMode</param>
        public LoginViewModel(IRegionManager regionManager, IUserRepository userRepository, ISnackbarMessageQueue snackbarMessageQueue, ILogger logger) 
        {
            RegionManager = regionManager;
            UserRepository = userRepository;
            SnackbarMessageQueue = snackbarMessageQueue;
            Logger = logger;
            LoginCommand = new DelegateCommand(async () => await Login());
            SetupCommand = new DelegateCommand(async () => await Setup());
        }

        /// <summary>
        /// Routes the user to the signup page
        /// </summary>
        /// <returns>Completed Task</returns>
        private async Task Setup()
        {
            Logger.Information("Navigating to the Sign Up View");
            RegionManager.RequestNavigate("ContentRegion", "SignUpView");
        }

        /// <summary>
        /// Attempts to log a user into the system
        /// </summary>
        /// <returns>Completed Task</returns>
        private async Task Login()
        {
            try
            {
                if (await UserRepository.CheckUserExists(UserName))
                {
                    if (await UserRepository.Login(UserName, Password))
                    {
                        UserName = null;
                        Password = null;
                        RegionManager.RequestNavigate("ContentRegion", "HomeView");
                    }
                    else
                    {
                        Logger.Information("Login failed");
                        SnackbarMessageQueue.Enqueue("Username or password incorrect");
                    }
                }
                else
                {
                    Logger.Information($"Login attempted for unregistered account {UserName}");
                    SnackbarMessageQueue.Enqueue("No account for this user");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception thrown in {nameof(Login)}",ex);
            }
            
        }

        /// <summary>
        /// Gets or sets if the navigation view model is the target
        /// </summary>
        /// <param name="navigationContext">The navigation context</param>
        /// <returns>bool indicating if the navigation was the intended target</returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// Triggered when navigating away from the View Model
        /// </summary>
        /// <param name="navigationContext">The navigation context</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            UserName = string.Empty;
            Password = string.Empty;
        }

        /// <summary>
        /// Triggered when navigating to the View Model
        /// </summary>
        /// <param name="navigationContext">The navigation context</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            UserName = string.Empty;
            Password = string.Empty;
        }


    }
}

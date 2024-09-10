using BudgetTracker.Entites;
using BudgetTracker.Services.Interfaces;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.ViewModels
{
    public class SignupViewModel : BindableBase, INavigationAware
    {
        private DelegateCommand backCommand;
        /// <summary>
        /// Gets or sets the command to go back to the login screen
        /// </summary>
        public DelegateCommand BackCommand
        {
            get => backCommand;
            set => SetProperty(ref backCommand, value);
        }

        private DelegateCommand signupCommand;
        /// <summary>
        /// Gets or sets the command to signup
        /// </summary>
        public DelegateCommand SignupCommand
        {
            get => signupCommand;
            set => SetProperty(ref signupCommand, value);
        }

        private string username;
        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string UserName
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private string password;
        //Gets or sets the password to login
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private string password1;
        //Gets or sets the password to login
        public string Password1
        {
            get => password1;
            set => SetProperty(ref password1, value);
        }

        private string email;
        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        /// <summary>
        /// Gets or sets the snackbart message queue
        /// </summary>
        public ISnackbarMessageQueue SnackbarMessageQueue { get; set; }

        /// <summary>
        /// Gets or sets the region manager
        /// </summary>
        public IRegionManager RegionManager { get; set; }

        /// <summary>
        /// Gets or sets the user repository
        /// </summary>
        public IUserRepository UserRepository { get; set; }

        /// <summary>
        /// Constructor for the signup view model
        /// </summary>
        /// <param name="regionManager">The region Manager to be used in view model</param>
        /// <param name="snackbarMessageQueue">The snackbar to be used in the view model</param>
        /// <param name="userRepository">The user repository to be used in the view model</param>
        public SignupViewModel(IRegionManager regionManager, ISnackbarMessageQueue snackbarMessageQueue, IUserRepository userRepository)
        {
            RegionManager = regionManager;
            SnackbarMessageQueue = snackbarMessageQueue;
            UserRepository = userRepository;
            ConstructDelegateCommands();

        }

        /// <summary>
        /// Creates the delegate commands
        /// </summary>
        private void ConstructDelegateCommands()
        {
            BackCommand = new DelegateCommand(async () => await Back());
            SignupCommand = new DelegateCommand(async () => await Signup());
        }

        /// <summary>
        /// Signs a user up to the application
        /// </summary>
        private async Task Signup()
        {
            try
            {
                if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Password1) && !string.IsNullOrEmpty(UserName))
                {
                    if (Password == Password1)
                    {
                        if (!await UserRepository.CheckUserExists(UserName, email))
                        {                           
                            if (await UserRepository.AddUser(CreateUser()))
                            {
                                Log.Information("Navigating to the home screen");
                                RegionManager.RequestNavigate("ContentRegion", "HomeView");
                            }
                            else
                            {
                                Log.Information("Error adding new user");
                                SnackbarMessageQueue.Enqueue("Error adding user");
                            }
                        }
                        else
                        {
                            Log.Information("Acount already exists");
                            SnackbarMessageQueue.Enqueue("Account already exists with these details");
                        }
                    }
                    else
                    {
                        Log.Information("Passwords do not match");
                        SnackbarMessageQueue.Enqueue("Passwords do not match");
                    }
                }
                else
                {
                    Log.Information("Not all fields set");
                    SnackbarMessageQueue.Enqueue("Please ensure all fields are set");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Exception thrown in {nameof(Signup)}", ex);
            }            
        }

        /// <summary>
        /// Creates a new user object
        /// </summary>
        /// <returns>New User object</returns>
        private User CreateUser()
        {
            return new User()
            {
                UserName = UserName,
                PasswordHash = Password,
                UserEmail = Email,
            };
        }

        /// <summary>
        /// Navigates back to the login screen
        /// </summary>
        /// <returns>Completed task</returns>
        private async Task Back()
        {
            try
            {
                Log.Information("Navigating to the login screen");
                RegionManager.RequestNavigate("ContentRegion", "LoginView");
            }
            catch (Exception ex)
            {
                Log.Error($"Exception thrown in {nameof(Back)}: {ex}",ex);
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
            Password = null;
            Password1 = null;
            Email = null;
            UserName = null;
        }

        /// <summary>
        /// Triggered when navigating to the View Model
        /// </summary>
        /// <param name="navigationContext">The navigation context</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Password = null;
            Password1 = null;
            Email = null;
            UserName = null;
        }
    }
}

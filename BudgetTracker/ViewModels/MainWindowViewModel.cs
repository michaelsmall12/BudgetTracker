using BudgetTracker.Views;
using Prism.Mvvm;
using Prism.Regions;

namespace BudgetTracker.ViewModels
{
    public class MainWindowViewModel : BindableBase, INavigationAware
    {
        private string _title = "Budget Tracker";
        /// <summary>
        /// Gets or sets the title for the application
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// Gets or sets the region manager
        /// </summary>
        public IRegionManager RegionManager { get; set; }

        /// <summary>
        /// Constructor for the Main Window View model
        /// </summary>
        /// <param name="regionManager">The region manager to be used in the view model</param>
        public MainWindowViewModel(IRegionManager regionManager)
        {
            RegionManager = regionManager;
            RegionManager.RequestNavigate("ContentRegion", "LoginView");
        }

        /// <summary>
        /// Triggered when navigating to the View Model
        /// </summary>
        /// <param name="navigationContext">The navigation context</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            RegionManager.RequestNavigate("ContentRegion", "LoginView");
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
            
        }
    }
}
